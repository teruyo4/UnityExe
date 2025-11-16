using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

// ゲームメインフェーズの定義
public class PlayingState : IState
{
    private GameManager _gm;
    private LevelSetting levelSetting;

    public PlayingState(GameManager gameManager) {
        _gm = gameManager;
        levelSetting = Resources.Load<LevelSetting>("Data/LevelSetting");
    }

    public void Enter() {
        _gm.records.Clear();
        _gm.SpawnFObj();
        _gm.changeCur();
        _gm.SpawnFObj();
        _gm.SetProgressRabbit();
        _gm.startTime = Time.time;
    }

    public void Tick() {
        if (Time.time - _gm.spawnTime > _gm.nextInterval) {
            _gm.SpawnFObj();
        }
    }

    public void Exit() {
    }

    public void BeCaught() {
        _gm.ChangeState(new GameOverState(_gm));
    }

    public void Goal() {
        _gm.ChangeState(new ClearState(_gm));
    }

    // UIからの入力を受付け式Objに送る。正解だった場合Objの交代を指示する。
    // UIに、不正解/１桁正解/正解を分けて音を出させるために、返り値を分ける。
    public int InputNumber(int n) {
        var ret = _gm.formulaList[0].InputNumber(n);
        if (ret == 2) {
            CorrectAnswer(_gm.formulaList[0]);
        }
        return ret;
    }
    
    private void CorrectAnswer(FormulaObj fo) {
        // 正解なので式Objリストから現状の式Objを外す。
        _gm.formulaList.Remove(fo);
        _gm.formulaList[0].changeCur();
        if (_gm.formulaList.Count == 1) {
            _gm.nextInterval = 0; // すぐ次の問題を出すためにインターバルなくす。
        }
        // 計算式と解答秒数を記録する。
        var diff = Time.time - _gm.startTime;
        _gm.records.Add(new Record(fo.lhs, fo.rhs, diff));
        // 回答までの時間でラビットの動きを変化させる。
        // 超速: １秒間、速度を+2で動かす。アニメ速度増し増し
        // 速し: １秒間、速度を+1で動かす。アニメ速度増し。
        // 普通: １秒間、速度は0にする。
        // 遅し: 速度は変わらない。
        if (diff < levelSetting.settings[LevelSetting.level].super) {
            _gm.chaseScene.ExecuteOperation(Grades.Super);
        } else if (diff < levelSetting.settings[LevelSetting.level].good) {
            _gm.chaseScene.ExecuteOperation(Grades.Good);
        } else if (diff < levelSetting.settings[LevelSetting.level].normal) {
            _gm.chaseScene.ExecuteOperation(Grades.Normal);
        } else {
            _gm.chaseScene.ExecuteOperation(Grades.Bad);
        }
        _gm.startTime = Time.time;
    }

}


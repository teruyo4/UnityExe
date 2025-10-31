using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

// ゲームメインフェーズの定義
public class PlayingState : IState
{
    private GameManager _gameManager;

    public PlayingState(GameManager gameManager) {
        _gameManager = gameManager;
    }

    public void Enter() {
        Debug.Log("Enter PlayingState");
        _gameManager.startTime = Time.time;
    }

    public void Tick() {
        if (Time.time - _gameManager.spawnTime > _gameManager.nextInterval) {
            _gameManager.SpawnFObj();
        }
    }

    public void Exit() {
        Debug.Log("Exit PlayingState");
    }

    public void BeCaught() {
        _gameManager.ChangeState(new GameOverState(_gameManager));
    }

    // UIからの入力を受付け式Objに送る。正解だった場合Objの交代を指示する。
    // UIに、不正解/１桁正解/正解を分けて音を出させるために、返り値を分ける。
    public int InputNumber(int n) {
        var ret = _gameManager.formulaList[0].InputNumber(n);
        if (ret == 2) {
            CorrectAnswer(_gameManager.formulaList[0]);
        }
        return ret;
    }
    
    private void CorrectAnswer(FormulaObj fo) {
        // 正解なら式Objリストから現状の式Objを外す。
        _gameManager.formulaList.Remove(fo);
        _gameManager.formulaList[0].changeCur();
        if (_gameManager.formulaList.Count == 1) {
            _gameManager.nextInterval = 0; // すぐ次の問題を出すためにインターバルなくす。
        }
        // 回答までの時間でラビットの動きを変化させる。
        // 超速: １秒間、速度を+2で動かす。アニメ速度増し増し
        // 速し: １秒間、速度を+1で動かす。アニメ速度増し。
        // 普通: １秒間、速度は0にする。
        // 遅し: 速度は変わらない。
        var diff = Time.time - _gameManager.startTime;
        if (diff < _gameManager.SuperTime) {
            _gameManager.chaseScene.ExecuteOperation(Grades.Super);
        } else if (diff < _gameManager.GoodTime) {
            _gameManager.chaseScene.ExecuteOperation(Grades.Good);
        } else if (diff < _gameManager.NormalTime) {
            _gameManager.chaseScene.ExecuteOperation(Grades.Normal);
        } else {
            _gameManager.chaseScene.ExecuteOperation(Grades.Bad);
        }
        _gameManager.startTime = Time.time;
    }

}


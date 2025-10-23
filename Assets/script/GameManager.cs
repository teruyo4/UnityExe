using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public FormulaObj formulaObj;
    public UIController kb;
    public rabbit rab, rabInst;
    public alice aliceObj, aliceInst;
    public ChaseScene chaseScene;
    public CutinScene cutinScene;
    public AudioClip[,] audioC = new AudioClip[9, 9];
    public float SuperTime;
    public float GoodTime;
    public float NormalTime;
    
    private FormulaObj formulaInst;
    private List<FormulaObj> formulaList = new List<FormulaObj>();
    private float startTime, spawnTime;
    
    private float intervalTime = 5.0f; // 次に出す問題までの期間
    private float nextInterval;
    private int phase = 0;

    void Awake() {
        // audio clip の読み込み
        for (int x = 0; x < 9; x++) {
            for (int y = 0; y < 9; y++) {
                audioC[x, y] = Resources.Load<AudioClip>($"voice/{x+1}x{y+1}");
            }
        }
    }

    void Start() {
        kb.SpawnStartLabel();  // タイトルとスタートボタンを表示
        phase = 0;
    }

    public async void GameStart() {
        var cts = new CancellationTokenSource();
        kb.SpawnKeyboard();
        cutinScene.StartScene();
        chaseScene.PreStartChase();
        await UniTask.Delay(3000, cancellationToken: cts.Token);
        chaseScene.StartChase();
        await UniTask.Delay(1000, cancellationToken: cts.Token);
        SpawnFObj();
        startTime = Time.time;
        formulaInst.changeCur();
        SpawnFObj();
        phase = 1;
    }
    
    public void GameReStart() {
        chaseScene.ClearCharacter();
        chaseScene.StartChase();
        SpawnFObj();
        formulaInst.changeCur();
        SpawnFObj();
        phase = 1;
    }

    public void becaught() {
        phase = 0;
        chaseScene.BeCaught();
        DestroyFObj();
        kb.SpawnFinished();
    }
    
    void FixedUpdate() {
        if (phase == 1) {
            if (Time.time - spawnTime > nextInterval) {
                SpawnFObj();
            }
        }
    }
    
    void SpawnFObj() {
        formulaInst = Instantiate(formulaObj);
        formulaInst.Setup();
        formulaList.Add(formulaInst);
        nextInterval = intervalTime;
        spawnTime = Time.time;
    }

    void DestroyFObj() {
        foreach (var obj in formulaList) {
            obj.FinishFormula();
        }
        formulaList.Clear();
    }

    // UIからの入力を受付け式Objに送る。正解だった場合Objの交代を指示する。
    // UIに、不正解/１桁正解/正解を分けて音を出させるために、返り値を分ける。
    public int InputNumber(int n) {
        var ret = formulaList[0].InputNumber(n);
        if (ret == 2) {
            CorrectAnswer(formulaList[0]);
        }
        return ret;
    }
    
    private void CorrectAnswer(FormulaObj fo) {
        // 正解なら式Objリストから現状の式Objを外す。
        formulaList.Remove(fo);
        formulaList[0].changeCur();
        if (formulaList.Count == 1) {
            nextInterval = 0; // すぐ次の問題を出すためにインターバルなくす。
        }
        // 回答までの時間でラビットの動きを変化させる。
        // 超速: １秒間、速度を+2で動かす。アニメ速度増し増し
        // 速し: １秒間、速度を+1で動かす。アニメ速度増し。
        // 普通: １秒間、速度は0にする。
        // 遅し: 速度は変わらない。
        var diff = Time.time - startTime;
        if (diff < SuperTime) {
            chaseScene.ExecuteOperation(Grades.Super);
        } else if (diff < GoodTime) {
            chaseScene.ExecuteOperation(Grades.Good);
        } else if (diff < NormalTime) {
            chaseScene.ExecuteOperation(Grades.Normal);
        } else {
            chaseScene.ExecuteOperation(Grades.Bad);
        }
        startTime = Time.time;
    }

}

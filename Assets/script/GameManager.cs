using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public FormulaObj formulaObj;
    public rabbit rab, rabInst;
    public alice aliceObj, aliceInst;
    public Back backImage;
    public KeyboardBack backImage2;
    public AudioClip[,] audioC = new AudioClip[9, 9];
    
    private FormulaObj formulaInst;
    private List<FormulaObj> formulaList = new List<FormulaObj>();
    private float startTime;
    
    private float intervalTime = 5.0f; // 次に出す問題までの期間
    private float nextInterval;

    void Awake() {
        // audio clip の読み込み
        for (int x = 0; x < 9; x++) {
            for (int y = 0; y < 9; y++) {
                audioC[x, y] = Resources.Load<AudioClip>($"voice/{x+1}x{y+1}");
            }
        }
    }
    
    void Start() {
        SpawnChar();
        SpawnFObj();
        formulaInst.changeCur();
        SpawnFObj();
    }

    void FixedUpdate() {
        if (Time.time - startTime > nextInterval) {
            SpawnFObj();
        }
    }
    
    void SpawnChar() {
        aliceInst = Instantiate(aliceObj);
        rabInst = Instantiate(rab);
        Instantiate(backImage);
        Instantiate(backImage2);
    }

    void SpawnFObj() {
        formulaInst = Instantiate(formulaObj);
        formulaInst.Setup();
        formulaList.Add(formulaInst);
        nextInterval = intervalTime;
        startTime = Time.time;
    }

    // UIからの入力を受付け式Objに送る。正解だった場合Objの交代を指示する。
    // UIに、不正解/１桁正解/正解を分けて音を出させるために、返り値を分ける。
    public int InputNumber(int n) {
        FormulaObj fo = formulaList[0];

        var ret = fo.InputNumber(n);
        if (ret == 2) {
            CorrectAnswer(fo);
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
        if (diff < 1.5f) {
            rabInst.ChangeBehaviour(0.004f, 50, 3f);
        } else if (diff < 2.0f) {
            rabInst.ChangeBehaviour(0.002f, 50, 2f);
        } else if (diff < 2.5f) {
            rabInst.ChangeBehaviour(0.0f, 50, 1f);
        } else {
            rabInst.ChangeBehaviour(-0.001f, 50, 1f);
        }
    }

}

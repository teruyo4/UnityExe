using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public FormulaObj formulaObj;
    public rabbit rab;
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
        SpawnFObj();
        formulaInst.changeCur();
        SpawnFObj();
    }

    void FixedUpdate() {
        if (Time.time - startTime > nextInterval) {
            SpawnFObj();
        }
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
        Debug.Log($"ret = {ret}.");
        return ret;
    }
    
    private void CorrectAnswer(FormulaObj fo) {
        // 正解なら式Objリストから現状の式Objを外す。
        formulaList.Remove(fo);
        formulaList[0].changeCur();
        if (formulaList.Count == 1) {
            nextInterval = 0; // すぐ次の問題を出すためにインターバルなくす。
        }
        // 回答までの時間で分岐
        var diff = Time.time - startTime;
        if (diff < 1.0f) {
            rab.accelX += 0.02f;
            Debug.Log("Excellent!!");
        } else if (diff < 1.5f) {
            rab.accelX += 0.01f;
            Debug.Log("Good!");
        } else if (diff < 2.5f) {
            rab.accelX += 0.0f;
            Debug.Log("Normal.");
        } else {
            rab.accelX -= 0.02f;
            Debug.Log("bad.");
        }
    }

}

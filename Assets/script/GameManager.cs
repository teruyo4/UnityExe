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
    public List<FormulaObj> formulaList = new List<FormulaObj>();

    public float SuperTime, GoodTime, NormalTime;
    
    private FormulaObj formulaInst;
    public float startTime, spawnTime;
    
    public float intervalTime = 5.0f; // 次に出す問題までの期間
    public float nextInterval;

    private IState _curState; // 現在のシーンフェーズを表す（タイトル、メイン、ゲームオーバー）

    void Awake() {
        LoadAudioClip();
    }

    void Start() {
        ChangeState(new JingleState(this));
    }

    void FixedUpdate() {
        _curState?.Tick();
    }
    
    // ゲームフェーズ（タイトル、メイン、ゲームオーバー）の遷移を行うメソッド
    public  void ChangeState(IState newState) {
        _curState?.Exit();    // 現在の状態がNULLでなければ、現状態の終了処理を実行する。
        _curState = newState; // 新状態に遷移。
        _curState.Enter();    // 新状態の開始処理を実行。
    }

    public IState GameAgent() {
        return _curState;
    }
    
    void LoadAudioClip() {
        // 9x9 audio clip の読み込み
        Debug.Log("load audio clip");
        for (int x = 0; x < 9; x++) {
            for (int y = 0; y < 9; y++) {
                audioC[x, y] = Resources.Load<AudioClip>($"voice/{x+1}x{y+1}");
            }
        }
    }

    public void SpawnFObj() {
        formulaInst = Instantiate(formulaObj);
        formulaInst.Setup();
        formulaList.Add(formulaInst);
        nextInterval = intervalTime;
        spawnTime = Time.time;
    }

    public void DestroyFObj() {
        foreach (var obj in formulaList) {
            obj.FinishFormula();
        }
        formulaList.Clear();
    }

    public void changeCur() {
        formulaInst.changeCur();
    }

}

using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

// State Machine パターンによるゲーム状態管理を行う。
// 状態インターフェーズの定義(メインフェーズ)
public class PlayingState: IState
{
    public UIController kb;
    public ChaseScene chaseScene;
    public CutinScene cutinScene;
    public FormulaObj formulaObj;

    private GameManager _gameManager;
    private FormulaObj formulaInst;
    private List<FormulaObj> formulaList = new List<FormulaObj>();
    private float intervalTime = 5.0f; // 次に出す問題までの期間
    private float nextInterval;

    public PlayingState(GameManager gameManager) {
        _gameManager = gameManager;
    }

    public void Enter() {
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
    }

    public void Tick() {
        if (Time.time - spawnTime > nextInterval) {
            SpawnFObj();
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

    public void Tick() {
    }

    public void Exit() {
        _gameManager.ChangeState(new GameOverState(_gameManager));
    }
    
}

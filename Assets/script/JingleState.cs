using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

// スタートジングルのフェーズエージェント定義
public class JingleState: IState
{
    private GameManager _gm;

    public JingleState(GameManager gameManager) {
        _gm = gameManager;
    }

    public async void Enter() {
        Debug.Log("Enter Jingle.");
        var cts = new CancellationTokenSource();
        _gm.kb.SpawnKeyboard();
        _gm.cutinScene.StartScene();
        _gm.chaseScene.PreStartChase();
        _gm.SetProgressRoad();
        Time.timeScale = 1.0f;
        await UniTask.Delay(3000, cancellationToken: cts.Token);
        _gm.chaseScene.StartChase();
        await UniTask.Delay(1000, cancellationToken: cts.Token);
        _gm.ChangeState(new PlayingState(_gm));
    }

    public void Tick() {
    }

    public void Exit() {
        Debug.Log("Exit Jingle.");
    }
}

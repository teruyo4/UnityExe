using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

// スタートジングルのフェーズエージェント定義
public class JingleState: IState
{
    private GameManager _gameManager;

    public JingleState(GameManager gameManager) {
        _gameManager = gameManager;
    }

    public async void Enter() {
        Debug.Log("Enter Jingle.");
        var cts = new CancellationTokenSource();
        _gameManager.kb.SpawnKeyboard();
        _gameManager.cutinScene.StartScene();
        _gameManager.chaseScene.PreStartChase();
        Time.timeScale = 1.0f;
        await UniTask.Delay(3000, cancellationToken: cts.Token);
        _gameManager.chaseScene.StartChase();
        await UniTask.Delay(1000, cancellationToken: cts.Token);
        _gameManager.SpawnFObj();
        _gameManager.changeCur();
        _gameManager.SpawnFObj();
        _gameManager.ChangeState(new PlayingState(_gameManager));
    }

    public void Tick() {
    }

    public void Exit() {
        Debug.Log("Exit Jingle.");
    }
}

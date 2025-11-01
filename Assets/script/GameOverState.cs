using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

// ゲームオーバーのフェーズエージェント定義
public class GameOverState: IState
{
    private GameManager _gameManager;

    public GameOverState(GameManager gameManager) {
        _gameManager = gameManager;
    }

    public void Enter() {
        Debug.Log("Enter GameOver.");
        _gameManager.DestroyFObj();
        _gameManager.chaseScene.GameOver();
        _gameManager.kb.SpawnFinished();
    }

    public void Tick() {
    }

    public void Exit() {
        Debug.Log("Exit GameOver.");
        _gameManager.chaseScene.ClearCharacter();
    }
}


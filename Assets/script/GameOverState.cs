using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

// ゲームオーバーのフェーズエージェント定義
public class GameOverState: IState
{
    public UIController kb;
    public ChaseScene chaseScene;

    private GameManager _gameManager;

    public GameOverState(GameManager gameManager) {
        _gameManager = gameManager;
    }

    public void Enter() {
        Debug.Log("Enter GameOver.");
        _gameManager.DestroyFObj();
        kb.SpawnFinished();
//        chaseScene.BeCaught();
    }

    public void Tick() {
    }

    public void Exit() {
        Debug.Log("Exit GameOver.");
    }
}


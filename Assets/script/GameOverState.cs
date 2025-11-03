using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

// ゲームオーバーのフェーズエージェント定義
public class GameOverState: IState
{
    private GameManager _gm;
//test
    public GameOverState(GameManager gameManager) {
        _gm = gameManager;
    }

    public void Enter() {
        Debug.Log("Enter GameOver.");
        _gm.DestroyFObj();
        _gm.chaseScene.GameOver();
        _gm.kb.SpawnFinished();
    }

    public void Tick() {
    }

    public void Exit() {
        Debug.Log("Exit GameOver.");
        _gm.chaseScene.ClearCharacter();
    }
}


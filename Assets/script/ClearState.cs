using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

// スタートジングルのフェーズエージェント定義
public class ClearState: IState
{
    private GameManager _gm;

    public ClearState(GameManager gameManager) {
        _gm = gameManager;
    }

    public void Enter() {
        Debug.Log("Enter ClearScene.");
        _gm.DestroyFObj();
        _gm.DelProgressRabbit();
        _gm.chaseScene.Clear();
        _gm.kb.SpawnClearDialog();
    }

    public void Tick() {
    }

    public void Exit() {
    }
}



using System;
using UnityEngine;

// State Machine パターンによるゲーム状態管理を行う。
// 状態インターフェーズの定義（タイトルフェーズ）
public class TitleState: IState
{
    private GameManager _gm;

    public TitleState(GameManager gameManager) {
        _gm = gameManager;
    }

    public void Enter() {
        Debug.Log("Enter Title.");
        _gm.kb.SpawnStartLabel();  // タイトル表示とスタートボタン割当
    }

    public void Tick() {
    }

    public void Exit() {
        Debug.Log("Exit Title.");
    }
}

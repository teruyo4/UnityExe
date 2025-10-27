using UnityEngine;

// State Machine パターンによるゲーム状態管理を行う。
// 状態インターフェーズの定義（タイトルフェーズ）
public class TitleState: IState
{
    public UIController kb;
    private GameManager _gameManager;

    public TitleState(GameManager gameManager) {
        _gameManager = gameManager;
    }

    public void Enter() {
        kb.SpawnStartLabel();  // タイトル表示とスタートボタン割当
    }

    public void Tick() {
    }

    public void Exit() {
        _gameManager.ChangeState(new PlayingState(_gameManager));
    }
    
}

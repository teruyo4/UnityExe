using System;
using UnityEngine;

// State Machine パターンによるゲーム状態管理を行う。
// 状態インターフェーズの定義
public interface IState
{
    // 状態に入った時に一度だけ実行される処理（初期化など）​
    void Enter();

    // 状態にいる間、毎フレーム実行される処理（Updateの代わり）​
    void Tick();

    // 状態を終了する時に一度だけ実行される処理（後片付けなど）​
    void Exit();
}

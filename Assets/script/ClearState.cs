using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

// スタートジングルのフェーズエージェント定義
public class ClearState: IState
{
    private GameManager _gm;
    private int[] answerNum = new int[10];
    private float[] average = new float[10];

    public ClearState(GameManager gameManager) {
        _gm = gameManager;
    }

    public void Enter() {
        Debug.Log("Enter ClearScene.");
        ClearPerform();
    }

    private async void ClearPerform() {
        var cts = new CancellationTokenSource();

        // 各種ゲームオブジェクトの動きを停止する。
        _gm.DestroyFObj();
        _gm.DelProgressRabbit();

        // 追跡シーンでクリア時小芝居。終わるまで３秒待つ。
        _gm.chaseScene.Clear();
        await UniTask.Delay(3000, cancellationToken: cts.Token);

        // カットイン
        _gm.chaseScene.ClearMusic();
        _gm.cutinScene.ClearScene();
        await UniTask.Delay(3000, cancellationToken: cts.Token);

        _gm.kb.SpawnClearDialog();
        CalcScore();
        DisplayScore();
    }

    private void CalcScore() {
        foreach (var rec in _gm.records) {
            answerNum[rec.lhs-1]++;
            average[rec.lhs-1] += rec.answerTime;
        }
        for (var i = 0; i < 9; i++) {
            if (answerNum[i] != 0) {
                average[i] = average[i] / answerNum[i];
            } else {
                average[i] = 0;
            }
        }
    }

    private void DisplayScore() {
        var root = _gm.kb._uiDocument.rootVisualElement;
        Label label = root.Q<Label>("NumOfAnswer1");
        label.text = $"１の段：{answerNum[0]}\n" +
                     $"２の段：{answerNum[1]}\n" +
                     $"３の段：{answerNum[2]}\n" +
                     $"４の段：{answerNum[3]}\n" +
                     $"５の段：{answerNum[4]}\n";
        label = root.Q<Label>("NumOfAnswer2");
        label.text = $"６の段：{answerNum[5]}\n" +
                     $"７の段：{answerNum[6]}\n" +
                     $"８の段：{answerNum[7]}\n" +
                     $"９の段：{answerNum[8]}\n";
        label = root.Q<Label>("AverageOfAnswer1");
        label.text = $"１の段：{average[0]:F2}秒\n" +
                     $"２の段：{average[1]:F2}秒\n" +
                     $"３の段：{average[2]:F2}秒\n" +
                     $"４の段：{average[3]:F2}秒\n" +
                     $"５の段：{average[4]:F2}秒\n";
        label = root.Q<Label>("AverageOfAnswer2");
        label.text = $"６の段：{average[5]:F2}秒\n" +
                     $"７の段：{average[6]:F2}秒\n" +
                     $"８の段：{average[7]:F2}秒\n" +
                     $"９の段：{average[8]:F2}秒\n";
    }

    public void Tick() {
    }

    public void Exit() {
        Debug.Log("Exit GameOver.");
        _gm.chaseScene.ClearCharacter();
    }
}



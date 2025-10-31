using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using DG.Tweening;

// キャラクターの１動作を示す
public struct Operation {
    public float speed;     // 動く速さ
    public float animSpeed; // アニメーションの早さ
    public int duration;  // 動作の続く時間（ミリ秒数）
}

public enum Grades {
    Super,
    Good,
    Normal,
    Bad,
    None
}

public struct CameraSize {
    public float sceneScale;
    public float posY;
    public float minDist;
    public float maxDist;
    public float bpm;
}

public class ChaseScene : MonoBehaviour {
    public rabbit rabObj, rabInst;
    public alice aliceObj, aliceInst;
    public AliceAndRabbit aar, aarInst;
    public Bgm bgm;
    public GameManager gm;
    
    private List<Operation> opeList;
    private List<CameraSize> csList;
    private int cameraPos = 1;
    private CancellationTokenSource cts;
    private const float staPos = 1.5f;

    void Start() {
        csList = new List<CameraSize>() {
            new CameraSize { sceneScale = 0.6f, posY = 0.4f, minDist = 3f, maxDist = 100f, bpm = 100f },
            new CameraSize { sceneScale = 0.8f, posY = 0.5f, minDist = 2f, maxDist = 4f, bpm = 120f },
            new CameraSize { sceneScale = 1.2f, posY = 0.6f, minDist = 1f, maxDist = 3f, bpm = 140f },
            new CameraSize { sceneScale = 1.5f, posY = 0.8f, minDist = 0f, maxDist = 2f, bpm = 160f }
        };
    }

    public void PreStartChase() {
        // 追跡エリアの拡大率セット。遠景からの開始とする。
        cameraPos = 0;
        transform.localScale = new Vector3(csList[cameraPos].sceneScale, csList[cameraPos].sceneScale, 0);

        // 追いかけキャラ実体化とアニメーションセット
        aliceInst = Instantiate(aliceObj, transform);
        aliceInst.transform.localPosition = new Vector3(-staPos - 6.0f, 0f, 0f);
        aliceInst.ChangeBehaviour(0f, 1.0f);

        // 逃げるキャラ実体化とアニメーションセット
        rabInst = Instantiate(rabObj, transform);
        rabInst.transform.localPosition = new Vector3(staPos - 6.0f, -0.1f, 0f);
        rabInst.ChangeBehaviour(0f, 1.0f);
        
        // スタートジングル音出し
        bgm.JingleMusic(180); 

        // キャラクターのスタート位置への移動
        DOTween.Sequence()
           .Append(aliceInst.transform.DOLocalMove(new Vector3(-staPos, 0f, 0f), 4.0f))
           .Join(rabInst.transform.DOLocalMove(new Vector3(staPos, 0f, 0f), 4.0f));
    }

    public void StartChase() {
        ExecuteOperation(Grades.None);
        aliceInst.opeFlag = true;
        rabInst.opeFlag = true;
        bgm.MainMusic(csList[cameraPos].bpm);
    }

    // 引数の指定に合わせて動作リストを作成する。
    public void ExecuteOperation(Grades grade) {
        cts?.Cancel();
        cts?.Dispose();
        cts = new();
        SetDefaultOperation();
        switch (grade) {
            case Grades.Super:
                opeList.Insert(0, new Operation { speed = 0.008f, animSpeed = 3.0f, duration = 1500 });
                break;
            case Grades.Good:
                opeList.Insert(0, new Operation { speed = 0.004f, animSpeed = 2.0f, duration = 1000 });
                break;
            case Grades.Normal:
                opeList.Insert(0, new Operation { speed = 0.001f, animSpeed = 1.2f, duration = 500 });
                break;
            default:
                break;
        }
        rabInst.SpawnKotodama(grade);
        ReflectOperation();
    }

    private void SetDefaultOperation() {
        opeList?.Clear();
        opeList = new List<Operation>() {
            new Operation { speed = -0.001f, animSpeed = 1.0f, duration = 1000 },
            new Operation { speed = -0.002f, animSpeed = 0.8f, duration = 1000 },
            new Operation { speed = -0.004f, animSpeed = 0.6f, duration = 0 }
        };
    }
    
    // 動作リストに沿ってキャラクターを動作させる。
    private async void ReflectOperation() {
        rabInst.ChangeBehaviour(opeList[0].speed, opeList[0].animSpeed);
        aliceInst.ChangeBehaviour(-opeList[0].speed, 1.0f);
        if (opeList.Count > 1) {
            await UniTask.Delay(opeList[0].duration, cancellationToken: cts.Token)
                .SuppressCancellationThrow()
                .ContinueWith(isCanceled => {
                    if (!isCanceled) {
                        opeList.RemoveAt(0);
                        ReflectOperation();
                    }
                });
        }
    }

    public void BeCaught() {
        var gameAgent = gm.GameAgent() as PlayingState;
        gameAgent.BeCaught();
    }

    public async void GameOver() {
        cts = new();
        ClearCharacter();
        aarInst = Instantiate(aar, transform);
        aarInst.transform.localPosition = new Vector3(0f, 0f, 0f);
        bgm.StopAudio();
        await UniTask.Delay(1000, cancellationToken: cts.Token);
        Time.timeScale = 0f;
        bgm.GameOverMusic(120f);
    }

    public void ClearCharacter() {
        if (aliceInst != null) aliceInst.remove();
        if (rabInst != null) rabInst.remove();
        if (aarInst != null) aarInst.remove();
    }

    void FixedUpdate() {
        if (rabInst == null || !rabInst.opeFlag)
            return;

        float charDist = rabInst.transform.localPosition.x -
                         aliceInst.transform.localPosition.x;

        if (charDist < csList[cameraPos].minDist)
            ChangeSituation(1);
        else if (charDist > csList[cameraPos].maxDist)
            ChangeSituation(-1);
    }

    // 状況変更（1:UP, 2:DOWN）
    void ChangeSituation(int status) {
        cameraPos += status;
        
        DOTween.Sequence()
            .Append(transform.DOScale(
                new Vector3(csList[cameraPos].sceneScale, csList[cameraPos].sceneScale, 0), 0.5f))
            .Join(transform.DOLocalMove(new Vector3(0f, csList[cameraPos].posY, 0f), 0.5f));
        bgm.Pitch(csList[cameraPos].bpm);
       
    }
}

using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using DG.Tweening;

using static Grades;

// キャラクターの１動作を示す
public struct Operation {
    public float speed;     // 動く速さ
    public float animSpeed; // アニメーションの早さ
    public int duration;    // 動作の続く時間（ミリ秒数）
    public float backSpeed; // 背景の動くスピード
}

public enum Grades {
    Super,
    Good,
    Normal,
    Bad,
    None
}

public class ChaseScene : MonoBehaviour {

    public rabbit rabObj, rabInst;
    public alice aliceObj, aliceInst;
    public AliceAndRabbit aar, aarInst;
    public GameManager gm;
    public Background background;
    
    private List<Operation> opeList;
    private List<ChaseOpeElem> csList;
    private int cameraPos = 1;
    private CancellationTokenSource cts;
    private const float staPos = 1.5f;

    void Awake() {
        csList = Resources.Load<ChaseOpe>("Data/ChaseOpe").Opes;
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
        gm.bgm.JingleMusic(180); 

        // キャラクターのスタート位置への移動
        DOTween.Sequence()
           .Append(aliceInst.transform.DOLocalMove(new Vector3(-staPos, 0f, 0f), 4.0f))
           .Join(rabInst.transform.DOLocalMove(new Vector3(staPos, 0f, 0f), 4.0f));
    }

    public void StartChase() {
        ExecuteOperation(None);
        aliceInst.opeFlag = true;
        rabInst.opeFlag = true;
        gm.bgm.MainMusic(csList[cameraPos].bpm);
    }

    // 引数の指定に合わせて動作リストを作成する。
    public void ExecuteOperation(Grades grade) {
        cts?.Cancel();
        cts?.Dispose();
        cts = new();
        SetDefaultOperation();
        switch (grade) {
            case Super:
                opeList.Insert(0, new Operation { speed = 0.008f, animSpeed = 3.0f, duration = 1500, backSpeed = 2.0f });
                break;
            case Good:
                opeList.Insert(0, new Operation { speed = 0.004f, animSpeed = 2.0f, duration = 1000, backSpeed = 1.5f });
                break;
            case Normal:
                opeList.Insert(0, new Operation { speed = 0.001f, animSpeed = 1.2f, duration = 500, backSpeed = 1.2f });
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
            new Operation { speed = -0.001f, animSpeed = 1.0f, duration = 1000, backSpeed = 1.0f },
            new Operation { speed = -0.002f, animSpeed = 0.8f, duration = 1000, backSpeed = 1.0f },
            new Operation { speed = -0.004f, animSpeed = 0.6f, duration = 0, backSpeed = 0.5f }
        };
    }
    
    // 動作リストに沿ってキャラクターを動作させる。
    private async void ReflectOperation() {
        rabInst.ChangeBehaviour(opeList[0].speed, opeList[0].animSpeed);
        aliceInst.ChangeBehaviour(-opeList[0].speed, 1.0f);
        background.ChangeScrollSpeed(opeList[0].backSpeed);
        Debug.Log($"bgspeed = {opeList[0].backSpeed}");
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

    public void Goal() {
        var gameAgent = gm.GameAgent() as PlayingState;
        gameAgent.Goal();
    }

    public void GameOver() {
        rabInst.opeFlag = false;
        aliceInst.opeFlag = false;
        ClearCharacter();
        aarInst = Instantiate(aar, transform);
        aarInst.transform.localPosition = new(0f, 0f, 0f);
        gm.bgm.StopAudio();
        background.StopScroll();
    }

    public void Clear() {
        var pos = rabInst.transform.localPosition + new Vector3(0.8f, 0f, 0f);
        rabInst.opeFlag = false;
        aliceInst.opeFlag = false;
        gm.bgm.RunAWayMusic();
        background.StopScroll();
        cts?.Cancel();
        DOTween.Sequence().Append(rabInst.transform.DOLocalMove(pos, 1.5f))
        .Join(rabInst.transform.DOLocalMoveY(-0.3f, 1.5f))
        .Join(rabInst.transform.DOScale(new Vector3(0.1f, 0.1f, 0f), 1.5f))
        .SetLink(rabInst.gameObject)
        .OnComplete(() => {
            Destroy(rabInst.gameObject);
        });
        pos = aliceInst.transform.localPosition + new Vector3(0.3f, -0.2f, 0f);
        DOTween.Sequence().Append(aliceInst.transform.DOLocalMove(pos, 2.0f))
        .Join(aliceInst.transform.DOScale(new Vector3(0.7f, 0.7f, 0f), 2.0f))
        .Append(aliceInst.transform.DOLocalMove(pos, 3.0f))
        .SetLink(aliceInst.gameObject)
        .OnComplete(() => {
            Destroy(aliceInst.gameObject);
        });
    }

    public void ClearMusic() {
        gm.bgm.ClearMusic();
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
            .Append(transform.parent.DOScale(
                new Vector3(csList[cameraPos].sceneScale, csList[cameraPos].sceneScale, 0), 0.5f));
        
        gm.bgm.Pitch(csList[cameraPos].bpm);
        background.ChangeScrollSpeed(csList[cameraPos].sceneScale);
    }
}

using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Random = UnityEngine.Random;

public class FormulaObj : MonoBehaviour {
    public AnswerBox answerBox;
    public GameManager gm;

    private AudioSource audioS;
    private int lhs, rhs, numAnswer;
    private int flag;
    private int numNext;     // 次にインプットすべき数字

    private Sequence seq;
    
    void Awake() {
        // 各種オブジェクトのアタッチ
        answerBox = (GameObject.FindWithTag("answerbox")).GetComponent<AnswerBox>();
        gm = (GameObject.FindWithTag("gamemanager")).GetComponent<GameManager>();
        audioS = GetComponent<AudioSource>();
    }

    // MakeProblem) 九九の問題を決める。
    void MakeProblem() {
        var num = Random.Range(0, 81);
        lhs = (int)(num / 9) + 1;
        rhs = (num % 9) +1;
        numAnswer = lhs * rhs;
    }

    public void Setup() {
        // 初期位置指定
        transform.position = new Vector3(0f, 4.50f, 0f);
        transform.localScale = new Vector3(0.1f, 0.1f, 0f);

        // 各初期値設定
        MakeProblem();
        var TMP = GetComponent<TextMeshPro>();
        TMP.text = $"{lhs}x{rhs}";
        numNext = (numAnswer > 9) ? (int)(numAnswer / 10) : numAnswer;
        flag = (numAnswer > 9) ? 0 : 1;

        // 動きの設定
        seq = DOTween.Sequence();
        seq.Join(transform.DOLocalMove(new Vector3(0f, 4.00f, 0f), 5.0f))
            .SetLink(gameObject);
    }

    // 自分の番が来たら、動きをズームアップに変更＆九九読上げの仕込み（１秒後に発音）。
    async public void changeCur() {
        seq.Kill();
        seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMove(new Vector3(0f, 2.00f, 0f), 5.0f))
            .Join(transform.DOScale(new Vector3(2f, 2f, 0f), 5.0f))
            .SetLink(gameObject);
        // 音声出力
        await UniTask.Delay(TimeSpan.FromSeconds(1.0f));
        audioS.clip = gm.audioC[lhs-1, rhs-1];
        audioS.Play();
    }
        
    public void FinishFormula() {
        // 外側に向けて回転させながらフェードアウト
        seq.Kill();
        seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMove(new Vector3(6f, 6f, 0f), 1.0f))
            .Join(transform.DOScale(new Vector3(0.1f, 0.1f, 0f), 1.0f))
            .SetLink(gameObject)
            .OnComplete(() => {
                Destroy(gameObject);
            });
    }

    // 押された数字キーに対する動作
    // 押すべき数字でなければ0を返し、十の位の入力なら１を、正解なら２を返す。
    public int InputNumber(int n) {
        int ret = 0;
        
        if (n == numNext) {
            if (flag == 0) {
                answerBox.PreAnswer(numNext);
                numNext = (lhs*rhs) % 10;
                flag = 1;
                ret = 1;
            } else {
                answerBox.Answer(lhs*rhs);
                FinishFormula();
                ret = 2;
            }
        }
        return ret;
    }
    
    
}

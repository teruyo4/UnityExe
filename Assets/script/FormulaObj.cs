using UnityEngine;
using TMPro;
using DG.Tweening;

public class FormulaObj : MonoBehaviour {
    public AnswerBox answerBox;

    private TextMeshPro tmp;
    private int lhs, rhs;
    private int flag;
    private int numNext;     // 次にインプットすべき数字
    
    
    void Start() {
        answerBox = (GameObject.FindWithTag("answerbox")).GetComponent<AnswerBox>();
    }

    public void Setup(int l, int r) {
        // 初期位置指定
        this.transform.position = new Vector3(0f, 5f, 0f);
        this.transform.localScale = new Vector3(0.1f, 0.1f, 0f);

        // 各初期値設定
        lhs = l;
        rhs = r;
        tmp = GetComponent<TextMeshPro>();
        tmp.text = $"{lhs}x{rhs}";
        numNext = (l*r > 9) ? (int)(l*r / 10) : l*r;
        flag = (l*r > 9) ? 0 : 1;

        // 動きの設定
        DOTween.Sequence()
            .Join(this.transform.DOLocalMove(new Vector3(0f, 3f, 0f), 5.0f))
            .Join(this.transform.DOScale(new Vector3(2f, 2f, 0f), 5.0f))
            .Play();
        
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
                //                MakeProblem();
                //                formulaInst.Setup(lhs, rhs);
                ret = 2;
            }
        }
        return ret;
    }
    
    
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int numFormula;  // 式番号
    public int lhs;
    public int rhs;
    public int numAnswer;   // 答え
    public int numNext;     // 次にインプットすべき数字
    public int flag;
    //    public FlyFormula flyFormula;
    public FormulaObj fo;
    public AnswerBox ab;
    
    void Start() {
        MakeProblem();
        fo.Setup(lhs, rhs);
    }

    // MakeProblem) ランダムに九九の問題を決める。
    void MakeProblem() {
        numFormula = Random.Range(0, 81);
        lhs = (int)(numFormula / 9) + 1; // 左項
        rhs = (numFormula % 9) +1;       // 右項
        numAnswer = lhs * rhs;              // 答え

        if (numAnswer > 9) {
            numNext = (int)(numAnswer / 10);
            flag = 0;
        } else {
            numNext = numAnswer;
            flag = 2;
        }
    }

    public int InputNumber(int n) {
        if (n != numNext) {
            return 0;
        } else {
            if (flag == 0) {
                ab.PreAnswer(numNext);
                numNext = numAnswer % 10;
                flag = 1;
                return 1;
            } else {
                ab.Answer(numAnswer);
                MakeProblem();
                fo.Setup(lhs, rhs);
            }
        }
        return 2;
    }
}

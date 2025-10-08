using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int lhs;
    public int rhs;
    public int numAnswer;   // 答え
    public int numNext;     // 次にインプットすべき数字
    public int flag;
    //    public AnswerBox answerBox;
    public FormulaObj formulaObj;
    private FormulaObj formulaInst;
    private List<FormulaObj> formulaList = new List<FormulaObj>();
    private int curFObj = 0;
    
    void Start() {
        MakeProblem();
        //        Debug.Log($"{lhs}x{rhs}");
        formulaInst = Instantiate(formulaObj);
        formulaInst.Setup(lhs, rhs);
        formulaList.Add(formulaInst);
    }

    // MakeProblem) ランダムに九九の問題を決める。
    void MakeProblem() {
        var num = Random.Range(0, 81);
        lhs = (int)(num / 9) + 1; // 左項
        rhs = (num % 9) +1;       // 右項
        numAnswer = lhs * rhs;              // 答え

        if (numAnswer > 9) {
            numNext = (int)(numAnswer / 10);
            flag = 0;
        } else {
            numNext = numAnswer;
            flag = 1;
        }
    }

    // 押された数字キーに対する動作
    // 押すべき数字でなければ0を返し、十の位の入力なら１を、正解なら２を返す。
    public int InputNumber(int n) {
        FormulaObj formulaInst;
        FormulaObj fo;
        int ret;
        
        fo = formulaList[curFObj];
        ret = fo.InputNumber(n);
        if (ret == 2) {
            formulaList.Remove(fo);
            Destroy(fo);

            MakeProblem();
            formulaInst = Instantiate(formulaObj);
            formulaInst.Setup(lhs, rhs);
            formulaList.Add(formulaInst);
        }

        return ret;
        
    }
}

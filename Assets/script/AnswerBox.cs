using UnityEngine;
using TMPro;
using DG.Tweening;

public class AnswerBox : MonoBehaviour {
    private TextMeshPro tmp;
    
    void Start() {
        //        Vector3 vec = new Vector3(0f, 2f, 0f);
        this.transform.position = new Vector3(0f, 2f, 0f);
        tmp = GetComponent<TextMeshPro>();
        tmp.text = "";
    }
    
    public void PreAnswer(int num) {
        tmp.text = $"{num}_";
        this.transform.DOKill();
        this.transform.position = new Vector3(0f, 2f, 0f);
        this.transform.localScale = new Vector3(3f, 3f, 1f);
        this.transform.DOScale(new Vector3(1f, 1f, 1f), 1f); 
    }

    public void Answer(int num) {
        tmp.text = $"{num}";
        this.transform.DOKill();
        this.transform.localScale = new Vector3(1f, 1f, 1f);
        this.transform.DOScale(new Vector3(0.05f, 0.05f, 1f), 2f); 
    }
}
//


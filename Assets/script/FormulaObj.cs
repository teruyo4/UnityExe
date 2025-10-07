using UnityEngine;
using TMPro;

public class FormulaObj : MonoBehaviour {
    private TextMeshPro tmp;
    
    void Start() {
        Vector3 vec = new Vector3(0f, 4f, 0f);
        this.transform.position = vec;

        tmp = GetComponent<TextMeshPro>();
    }
    
    public void Setup(int lhs, int rhs) {
        string f = $"{lhs}x{rhs}";
        Debug.Log(f);
        tmp.text = f;
    }
}

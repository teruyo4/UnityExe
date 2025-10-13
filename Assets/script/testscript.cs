using UnityEngine;

public class testscript : MonoBehaviour {
    public int a;
    
    void Start() {
        a=0;
    }

    void Update() {
        Debug.Log($"a = {a}");
    }

    void setA(int x) {
        a = x;
    }
}

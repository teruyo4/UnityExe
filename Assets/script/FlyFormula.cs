using System;
using System.Collections.Generic;
using UnityEngine;

public class FlyFormula : MonoBehaviour
{
    private int numFormula;
    private Sprite[] sprites;
    
    void Start() {
        Vector3 vec = new Vector3(0f, 4f, 0f);
        this.transform.position = vec;
    }
    
    public void Setup(int target) {
        numFormula = target;
        var img = this.GetComponent<SpriteRenderer>();
        var resq = $"q99_{numFormula}";
        Debug.Log(resq);

        sprites = Resources.LoadAll<Sprite>("images/q99");
        img.sprite = Array.Find(sprites, e => e.name == resq);

    }
}

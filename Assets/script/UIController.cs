using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject sampleElement;
    [SerializeField] GameManager gm;
    
    private UIDocument _uiDocument;
    
    void Start() {
        var bo = Instantiate(sampleElement);
        _uiDocument = bo.GetComponent<UIDocument>();

        // set click function
        for (int i = 0; i < 10; i++) {
            var btn = _uiDocument.rootVisualElement.Q<Button>($"button{i}");
            int local_i = i;
            btn.clickable.clicked += () => {InputNumberLine(local_i);};
        }
    }

    void InputNumberLine(int num) {
        switch (gm.InputNumber(num)) {
            case 0:
                break;
            case 1:
                GetComponent<AudioSource>().Play();
                break;
            case 2:
                GetComponent<AudioSource>().Play();
                break;
        }
    }
}

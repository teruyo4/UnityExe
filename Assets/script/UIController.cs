using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject sampleElement;
    [SerializeField] GameObject startLabel;
    [SerializeField] GameObject finished;
    [SerializeField] GameManager gm;
    
    private UIDocument _uiDocument;
    private GameObject ko;
    private GameObject sl;
    
    public void SpawnStartLabel() {
        var sl = Instantiate(startLabel);
        _uiDocument = sl.GetComponent<UIDocument>();

        var btn = _uiDocument.rootVisualElement.Q<Button>($"startbutton");
        btn.clickable.clicked += () => {
            Destroy(sl);
            gm.GameStart();
        };
    }
        
    public void SpawnFinished() {
        var fl = Instantiate(finished);
        _uiDocument = fl.GetComponent<UIDocument>();

        var btn = _uiDocument.rootVisualElement.Q<Button>($"retrybutton");
        btn.clickable.clicked += () => {
            Destroy(fl);
            gm.GameReStart();
        };
    }
        
    public void SpawnKeyboard() {
        var ko = Instantiate(sampleElement);
        _uiDocument = ko.GetComponent<UIDocument>();

        // set click function
        for (int i = 0; i < 10; i++) {
            var btn = _uiDocument.rootVisualElement.Q<Button>($"button{i}");
            int local_i = i;
            btn.clickable.clicked += () => {InputNumberLine(local_i);};
        }
    }

    public void DestroyKeyboard() {
        Destroy(ko);
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

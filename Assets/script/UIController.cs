using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject sampleElement;
    private UIDocument _uiDocument;
    private Label numLabel;
    
    void Start() {
	var bo = Instantiate(sampleElement);
	_uiDocument = bo.GetComponent<UIDocument>();

	// set click function
	foreach (int i in new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9}) {
	    var btn = _uiDocument.rootVisualElement.Q<Button>($"button{i}");
	    btn.clickable.clicked += () => {InputNumberLine(i);};
	}
	numLabel = _uiDocument.rootVisualElement.Q<Label>("NumLabelStr");
	numLabel.text = "";
    }

    void InputNumberLine(int num) {
	string str = (string)numLabel.text;
	if (str.Length > 7)
	    str = str.Substring(1, 7);
	numLabel.text = $"{str}{num}";
    }
}

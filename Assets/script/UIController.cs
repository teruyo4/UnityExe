using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject sampleElement;
    private UIDocument _uiDocument;
    
    void Start()
    {
	var bo = Instantiate(sampleElement);
	_uiDocument = bo.GetComponent<UIDocument>();
	var btn = _uiDocument.rootVisualElement.Q<Button>("button1");
	btn.clickable.clicked += () => {Debug.Log("clicked");};
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

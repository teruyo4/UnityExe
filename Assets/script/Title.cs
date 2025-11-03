using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Title : MonoBehaviour {
    
    [SerializeField] GameObject TitleButton;
    private UIDocument _uiDocument;
    
    void Start() {
        var titleButton = Instantiate(TitleButton);

        _uiDocument = titleButton.GetComponent<UIDocument>();
        var btn = _uiDocument.rootVisualElement.Q<Button>("TapToStart");
        btn.clickable.clicked += () => {
            Destroy(_uiDocument);
            SceneManager.LoadScene("SampleScene");
        };
    }
}

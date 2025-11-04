using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelSelect : MonoBehaviour {

    [SerializeField] GameObject LevelSelectUI;
    
    private UIDocument _uiDocument;
    private int levelSelected = 0;

    void Start() {
        var levelSelectUI = Instantiate(LevelSelectUI);
        _uiDocument = levelSelectUI.GetComponent<UIDocument>();
        SetButton();
        SetRadioButtonGroup();
    }

    private void SetRadioButtonGroup() {
        var radioGroup = _uiDocument.rootVisualElement.Q<RadioButtonGroup>("LevelSelect");
        radioGroup.RegisterValueChangedCallback(evt => {
            levelSelected = evt.newValue;
            Debug.Log($"Level: {evt.newValue}");
        });
        radioGroup.value = levelSelected;
    }
        
    private void SetButton() {
        // スタートボタン設定
        var btn = _uiDocument.rootVisualElement.Q<Button>("Start");
        btn.clickable.clicked += () => {
            Destroy(_uiDocument);
            SceneManager.LoadScene("SampleScene");
        };

        // 設定ボタンの設定
        btn = _uiDocument.rootVisualElement.Q<Button>("Setting");
        btn.clickable.clicked += () => {
            Destroy(_uiDocument);
            SceneManager.LoadScene("SettingScene");
        };

        // 終了ボタンの設定
        btn = _uiDocument.rootVisualElement.Q<Button>("Exit");
        btn.clickable.clicked += () => {
            Destroy(_uiDocument);
            Application.Quit();
        };
    }
}

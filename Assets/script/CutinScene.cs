using UnityEngine;

public class CutinScene : MonoBehaviour {
    public CutinStart cutinStartObj, cutinStartInst;
    public CutinClear cutinClearObj, cutinClearInst;

    private const float CutinY = 2.5f;
    
    public void StartScene() {
        cutinStartInst = Instantiate(cutinStartObj, transform);
        cutinStartInst.transform.localPosition = new Vector3(-5f, 0f, 0f);

        cutinStartInst.Operation();
    }

    public void ClearScene() {
        cutinClearInst = Instantiate(cutinClearObj, transform);
        cutinClearInst.transform.localPosition = new Vector3(-5f, 0f, 0f);

        cutinClearInst.Operation();
    }
}

using DG.Tweening;
using TMPro;
using UnityEngine;

public class MileStoneImage : MonoBehaviour {
    public ProgressText1 pt1;
    public ProgressText2 pt2;
    public ProgressRoad progressRoad;

    void Start() {
        transform.localPosition = new Vector3(4f, -0.3f, 0f);
        transform.DOLocalMove(new Vector3(-5f, -0.3f, 0f), 4f)
        .SetEase(Ease.Linear)
        .OnComplete(() => {
            Destroy(gameObject);
        });

        Instantiate(pt1, transform);
        var pt2inst = Instantiate(pt2, transform);
        pt2inst.GetComponent<TextMeshPro>().fontSize = 6;
        progressRoad = (GameObject.FindWithTag("progressroad")).GetComponent<ProgressRoad>();
        pt2inst.GetComponent<TextMeshPro>().text = $" {progressRoad.GetLeftMile()}";
    }
}


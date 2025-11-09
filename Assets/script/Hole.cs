using DG.Tweening;
using UnityEngine;

public class Hole : MonoBehaviour {
    void Start() {
        transform.localPosition = new Vector3(4f, 0f, 0f);
        transform.DOLocalMove(new Vector3(0f, 0f, 0f), 4f)
        .SetEase(Ease.Linear)
        .OnComplete(() => {
            Destroy(gameObject);
        });
    }

    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Goal!");
        if (col.gameObject.tag == "rabbit") {
            transform.parent.GetComponent<ChaseScene>().Goal();
        }
    }
}

using DG.Tweening;
using UnityEngine;

public class Hole : MonoBehaviour {
    void Start() {
        transform.localPosition = new Vector3(4f, 0.1f, 0f);
        transform.DOLocalMove(new Vector3(0f, 0.1f, 0f), 4f)
        .SetEase(Ease.Linear)
        .OnComplete(() => {
            Destroy(gameObject);
        });
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "rabbit") {
            transform.DOPause();
            transform.parent.GetComponent<ChaseScene>().Goal();
        }
    }
}

using UnityEngine;
using DG.Tweening;

public class CutinStart : MonoBehaviour {
    public void Operation() {
        var seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMove(new Vector3(0f, 0f, 0f), 2f))
            .Append(transform.DOLocalMove(new Vector3(8f, 8f, 0f), 1f))
            .Join(transform.DOLocalRotate(new Vector3(8f, 0f, 720f), 1f, RotateMode.FastBeyond360))
            .SetLink(gameObject)
            .OnComplete(() => {
                Destroy(gameObject);
            });
    }

}


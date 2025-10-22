using UnityEngine;
using DG.Tweening;

public class rabbit : MonoBehaviour
{
    public GameObject superKotodama;
    public GameObject goodKotodama;
    public GameObject okKotodama;
    public GameObject badKotodama;
    
    private float speedX; // 移動速度

    void Start() {
        ChangeBehaviour(0f, 0f);
    }

    void FixedUpdate() {
        transform.Translate(speedX, 0f, 0f);
    }

    // 挙動を変える
    public void ChangeBehaviour(float sp, float animsp) {
        var anim = this.gameObject.GetComponent<Animator>();
        anim.speed = animsp;
        speedX = sp;
    }

    public void SpawnKotodama(Grades grade) {
        GameObject kotodama;
        switch (grade) {
            case Grades.Super:
                kotodama = superKotodama;
                break;
            case Grades.Good:
                kotodama = goodKotodama;
                break;
            case Grades.Normal:
                kotodama = okKotodama;
                break;
            default:
                kotodama = badKotodama;
                break;
        }
        var inst = Instantiate(kotodama, transform);
        inst.transform.localPosition = new Vector3(0.4f, 0f, 0f);
        inst.transform.DOLocalMove(new Vector3(0f, 0.5f, 0f), 1.0f)
            .SetRelative(true)
            .SetLink(inst)
            .OnComplete(() => {
                Destroy(inst);
            });
                                            }

    public void remove() {
        Destroy(this.gameObject);
    }
}

using UnityEngine;
using DG.Tweening;

public class rabbit : MonoBehaviour
{
    public GameObject superKotodama;
    public GameObject goodKotodama;
    public GameObject okKotodama;
    public GameObject badKotodama;
	public bool opeFlag;
    
    private float speedX; // 移動速度

    void Start()
    {
        opeFlag = false;
    }

    void FixedUpdate()
	{
		if (opeFlag)
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
            case Grades.Bad:
                kotodama = badKotodama;
                break;
            default:
                return;
        }
        var inst = Instantiate(kotodama, transform);
        var renderer = inst.GetComponent<SpriteRenderer>();
        inst.transform.localPosition = new Vector3(0.4f, 0f, 0f);
        DOTween.Sequence()
        .Append(
            inst.transform.DOLocalMove(new Vector3(0f, 0.5f, 0f), 3.0f)
            .SetRelative(true)
            .SetLink(inst)
            .OnComplete(() => {
                Destroy(inst);
            }))
            .Join(renderer.DOFade(0.0f, 3.0f));
                                            }

    public void remove() {
        Destroy(this.gameObject);
    }
}

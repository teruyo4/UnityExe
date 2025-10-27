using UnityEngine;
using DG.Tweening;

public class rabbit : MonoBehaviour
{
    public GameObject superKotodama;
    public GameObject goodKotodama;
    public GameObject okKotodama;
    public GameObject badKotodama;
	public bool opeFlag;

    private AudioSource audioS;
	private float speedX; // 移動速度
    private AudioClip[] rabVoice = new AudioClip[4];

    void Start()
    {
        opeFlag = false;
        audioS = GetComponent<AudioSource>();
		rabVoice[0] = Resources.Load<AudioClip>("voice/super");
		rabVoice[1] = Resources.Load<AudioClip>("voice/good");
		rabVoice[2] = Resources.Load<AudioClip>("voice/normal");
		rabVoice[3] = Resources.Load<AudioClip>("voice/bad");
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
				audioS.clip = rabVoice[0];
                break;
            case Grades.Good:
                kotodama = goodKotodama;
				audioS.clip = rabVoice[1];
                break;
            case Grades.Normal:
                kotodama = okKotodama;
				audioS.clip = rabVoice[2];
                break;
            case Grades.Bad:
                kotodama = badKotodama;
				audioS.clip = rabVoice[3];
                break;
            default:
                return;
        }
        var inst = Instantiate(kotodama, transform);
        var renderer = inst.GetComponent<SpriteRenderer>();
        inst.transform.localPosition = new Vector3(0.4f, 0f, 0f);
		audioS.Play();
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

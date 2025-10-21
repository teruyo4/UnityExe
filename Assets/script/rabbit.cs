using UnityEngine;

public class rabbit : MonoBehaviour
{
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

    public void remove() {
        Destroy(this.gameObject);
    }
}

using UnityEngine;

public class rabbit : MonoBehaviour
{
    private float speedX;
    private int puTime;
    private float posx;

    private float normalsp = -0.002f;

    void Start() {
        ChangeBehaviour(normalsp, 0, 1f);
    }

    // 0.02秒に１回呼ばれる。
    void FixedUpdate() {
        var vec3 = this.transform.position;
        vec3.x += speedX;
        this.transform.position = vec3;
        posx = vec3.x;
        if (puTime > 0) {
            puTime--;
            if (puTime <= 0) {
                ChangeBehaviour(normalsp, 0, 1f);
            }
        }
    }

    // 挙動を変える
    public void ChangeBehaviour(float sp, int ti, float animsp) {
        var anim = this.gameObject.GetComponent<Animator>();
        speedX = sp;
        puTime = ti;
        anim.speed = animsp;
    }
}

using UnityEngine;

public class ProgressRoad : MonoBehaviour {

    public ProgressBack progressBack;
    public ProgressRabbit progressRabbit;
    public MileStoneImage mileStoneImage;
    public ChaseScene chaseScene;
    public Hole hole;

    private ProgressBack progressBackInst;
    private ProgressRabbit progressRabbitInst;

    private Rigidbody2D rbRabbit;
    private Animator rbAnim;

    private float mileStone;

    private static float goalMeter = 100f;

    public float GetLeftMile() {
        return goalMeter - mileStone;
    }
    
    public void SetProgressRoad() {
        progressBackInst = Instantiate(progressBack, transform);
    }

    public void SetProgressRabbit() {
        progressRabbitInst = Instantiate(progressRabbit, transform);
        progressRabbitInst.transform.localPosition = new Vector3(-2f, 0f, 0f);
        rbRabbit = progressRabbitInst.GetComponent<Rigidbody2D>();
        rbRabbit.AddForce(new Vector2(0.1f, 0f), ForceMode2D.Impulse);
        rbAnim = progressRabbitInst.GetComponent<Animator>();
        rbAnim.speed = 5f;

        mileStone = 100f;
    }

    public void DelProgressRabbit() {
        Debug.Log("DelprogressRabit");
        Destroy(progressRabbitInst);
        progressRabbitInst = null;
    }

    void FixedUpdate() {
        if (progressRabbitInst != null && progressRabbitInst.progressMeter() >= mileStone) {
            if (mileStone >= goalMeter) {
                Instantiate(hole, chaseScene.transform);
            } else {
                var ms = Instantiate(mileStoneImage, chaseScene.transform);
            }
            mileStone += 100f;
        }
       
    }
    
}

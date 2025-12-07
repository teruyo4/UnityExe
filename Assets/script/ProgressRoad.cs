using UnityEngine;

public class ProgressRoad : MonoBehaviour {

    public GameParameter gameParameter;
    
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
    private float leftMile;

    private MileStoneImage ms;

    public float GetLeftMile() {
        return leftMile;
    }
    
    public void SetProgressRoad() {
        progressBackInst = Instantiate(progressBack, transform);
    }

    public void SetProgressRabbit() {
        progressRabbitInst = Instantiate(progressRabbit, transform);
        progressRabbitInst.transform.localPosition = new Vector3(-2f, 0f, 0f);
        rbRabbit = progressRabbitInst.GetComponent<Rigidbody2D>();
        rbAnim = progressRabbitInst.GetComponent<Animator>();
        rbAnim.speed = 5f;

        mileStone = 100f;
    }

    public void DelProgressRabbit() {
        Destroy(progressRabbitInst.gameObject);
        progressRabbitInst = null;
    }

    void FixedUpdate() {
        // チェックポイントを超えるタイミングで、標識出したりゴールの穴を出したりする。
        if (progressRabbitInst != null && progressRabbitInst.progressMeter() >= mileStone) {
            if (mileStone >= gameParameter.RoadDistance) {
                Instantiate(hole, chaseScene.transform);
            } else {
                ms = Instantiate(mileStoneImage, chaseScene.transform);
                leftMile = gameParameter.RoadDistance - mileStone;
            }
            // チェックポイントを100メートル先に再設定する。
            mileStone += 100f;
        }
       
    }

    public void DelMileStone() {
        if (ms != null) {
            Destroy(ms.gameObject);
        }
    }

    public float ProgressRoadSize() {
        return progressBackInst.size.x;
    }
}

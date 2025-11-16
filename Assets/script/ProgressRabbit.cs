using UnityEngine;

public class ProgressRabbit : MonoBehaviour {

    public GameParameter gameParameter;
    
    private float baseSpeed, baseLocalSpeed;
    private float pMeter;
    
    void Start() {
        var sizex = transform.parent.GetComponent<ProgressRoad>().ProgressRoadSize();
        baseSpeed = gameParameter.BaseSpeed * Time.fixedDeltaTime;
        baseLocalSpeed = baseSpeed * sizex / gameParameter.RoadDistance;
        pMeter = 0f;
        Debug.Log($"baseSpeed={baseSpeed}, baseLocalSpeed={baseLocalSpeed}");
    }

    void FixedUpdate() {
        Vector3 pos = transform.localPosition;
        pos.x += baseLocalSpeed;
        transform.localPosition = pos;
        pMeter += baseSpeed;
    }

    public float progressMeter() {
        //        return (transform.localPosition.x + 2f) * 250f;
        return pMeter;
    }
}

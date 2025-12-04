using UnityEngine;

public class ProgressRabbit : MonoBehaviour {

    public GameParameter gameParameter;
    
    private float baseSpeed, baseLocalSpeed;
    private float pMeter;
    
    void Start() {
        // sizex : 実際の進捗バーのスクリーン上の長さ
        // gameParameter.RoadDistance : 理論的な道程の長さ（１０００）
        // baseSpeed : BaseSpeed の fixedDeltaTime 分の距離
        // baseLocalSpeed : fixedDeltaTimeあたり進む距離
        var sizex = transform.parent.GetComponent<ProgressRoad>().ProgressRoadSize();
        baseSpeed = gameParameter.BaseSpeed * Time.fixedDeltaTime;
        baseLocalSpeed = baseSpeed * sizex / gameParameter.RoadDistance;
        pMeter = 0f;
        Debug.Log($"baseSpeed={baseSpeed}, baseLocalSpeed={baseLocalSpeed}");
    }

    void FixedUpdate() {
        // 毎回 baseLocalSpeed 分だけ進めて、理論上の距離 pMeter も進める。
        Vector3 pos = transform.localPosition;
        pos.x += baseLocalSpeed;
        transform.localPosition = pos;
        pMeter += baseSpeed;
    }

    public float progressMeter() {
        return pMeter;
    }
}

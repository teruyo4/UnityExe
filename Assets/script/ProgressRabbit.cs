using UnityEngine;

public class ProgressRabbit : MonoBehaviour {
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public float progressMeter() {
        return (transform.position.x + 2f) * 250f;
    }
}

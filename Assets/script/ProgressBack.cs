using UnityEngine;

public class ProgressBack : MonoBehaviour {

    public Vector3 size;
    
    void Start() {
        size = gameObject.GetComponent<Renderer>().bounds.size;
        Debug.Log($"x={size.x}, y={size.y}");
    }

    // Update is called once per frame
    void Update() {
        
    }
}

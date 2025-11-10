using EasyParallax;
using UnityEngine;

public class Background : MonoBehaviour {

    private SpriteDuplicator[] scene;

    void Awake() {
        scene = GetComponentsInChildren<SpriteDuplicator>();
    }
    
    public void StopScroll() {
        foreach (SpriteDuplicator spriteDuplicator in scene) {
            spriteDuplicator.PauseMovement();
        }
    }
}

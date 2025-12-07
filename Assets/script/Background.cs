using EasyParallax;
using UnityEngine;

public class Background : MonoBehaviour {

    private SpriteDuplicator[] scene;

    void Awake() {
        scene = GetComponentsInChildren<SpriteDuplicator>();
    }
    
    public void StopScroll() {
        ChangeScrollSpeed(0f);
    }

    public void ChangeScrollSpeed(float magnification) {
        foreach (SpriteDuplicator spriteDuplicator in scene) {
            Debug.Log($"mag = {magnification}");
            spriteDuplicator.PlayMovement(magnification);
        }
    }
}

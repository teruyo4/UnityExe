using UnityEngine;
using EasyParallax;

public class TestScript : MonoBehaviour {

    public GameObject sky;
    public GameObject clouds;
    public GameObject mountainFar;
    public GameObject mountainClose;

    public int count = 0;
    
    void Update() {
        if (count == 200) {
            StopScroll();
        } else if (count == 300) {
            PlayScroll();
        }
        count++;
    }

    void StopScroll() {
        sky.GetComponent<SpriteDuplicator>().PauseMovement();
        clouds.GetComponent<SpriteDuplicator>().PauseMovement();
        mountainFar.GetComponent<SpriteDuplicator>().PauseMovement();
        mountainClose.GetComponent<SpriteDuplicator>().PauseMovement();
    }

    void PlayScroll() {
        sky.GetComponent<SpriteDuplicator>().PlayMovement();
        clouds.GetComponent<SpriteDuplicator>().PlayMovement();
        mountainFar.GetComponent<SpriteDuplicator>().PlayMovement();
        mountainClose.GetComponent<SpriteDuplicator>().PlayMovement();
    }
}

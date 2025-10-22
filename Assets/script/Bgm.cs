using UnityEngine;

public class Bgm : MonoBehaviour {

    AudioSource audioSource;
    
    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartMusic() {
        audioSource.Play();
    }
}

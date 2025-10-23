using UnityEngine;

public class Bgm : MonoBehaviour {

    [SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioClip mainBgm;
	[SerializeField] private AudioClip jingle1;
    
    public void StartMusic() {
        audioSource.PlayOneShot(jingle1);
    }

    public void BGM() {
        audioSource.PlayOneShot(mainBgm);
    }

    public void Pitch(float bpm) {
        audioSource.pitch = bpm / 120f;
    }
}

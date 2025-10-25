using UnityEngine;

public class Bgm : MonoBehaviour {

    [SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioClip mainBgm;
	[SerializeField] private AudioClip jingle1;
	[SerializeField] private AudioClip overBgm;
    
    public void StartMusic() {
        audioSource.PlayOneShot(jingle1);
    }

    public void BGM() {
        audioSource.PlayOneShot(mainBgm);
    }

    public void StopBGM() {
        audioSource.Stop();
    }

    public void GameOverMusic() {
        audioSource.PlayOneShot(overBgm);
    }

    public void Pitch(float bpm) {
        audioSource.pitch = bpm / 120f;
    }
}

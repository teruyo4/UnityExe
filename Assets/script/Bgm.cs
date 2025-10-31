using UnityEngine;

public class Bgm : MonoBehaviour {

    [SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioClip mainBgm;
	[SerializeField] private AudioClip jingle1;
	[SerializeField] private AudioClip overBgm;
    
    public void JingleMusic(float pitch) {
        StopAudio();
        Pitch(pitch);
        audioSource.PlayOneShot(jingle1);
    }

    public void MainMusic(float pitch) {
        StopAudio();
        Pitch(pitch);
        audioSource.PlayOneShot(mainBgm);
    }

    public void StopAudio() {
        audioSource.Stop();
    }

    public void GameOverMusic(float pitch) {
        StopAudio();
        Pitch(pitch);
        audioSource.PlayOneShot(overBgm);
    }

    public void Pitch(float bpm) {
        audioSource.pitch = bpm / 120f;
    }
}

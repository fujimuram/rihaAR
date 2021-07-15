using UnityEngine;

public class HitPlaySound : MonoBehaviour {

    public AudioClip sound;
    public float volume;

    void OnCollisionEnter(Collision collision) {
        AudioSource.PlayClipAtPoint(sound, transform.position, volume);
    }
}
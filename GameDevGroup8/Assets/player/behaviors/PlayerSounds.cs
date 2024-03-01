using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] AudioClip[] damageSounds;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playDamageSound()
    {
        AudioClip clip = damageSounds[UnityEngine.Random.Range(0, damageSounds.Length)];
        audioSource.pitch = (Random.Range(0.8f,1.2f));
        audioSource.PlayOneShot(clip);
    }


}

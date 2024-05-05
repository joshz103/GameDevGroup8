using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] AudioClip[] damageSounds;
    [SerializeField] AudioClip[] swingSounds;
    [SerializeField] AudioClip[] stepSounds;
    [SerializeField] AudioClip[] landSounds;
    [SerializeField] AudioClip soundClip;

    [SerializeField] private AudioSource audioSource1;
    [SerializeField] private AudioSource audioSource2;

    // Start is called before the first frame update
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        //audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    public void playDamageSound()
    {
        if (audioSource1.isPlaying) //Prevents sounds from stacking and becoming loud when attacking multiple enemies at once.
        {
            audioSource1.Stop();
        }

        AudioClip clip = damageSounds[UnityEngine.Random.Range(0, damageSounds.Length)];
        audioSource1.pitch = (Random.Range(0.8f,1.2f));
        audioSource1.volume = 0.4f;
        audioSource1.PlayOneShot(clip);
    }

    public void playCritSound()
    {
        AudioClip clip = soundClip;
        audioSource2.volume = 1f;
        audioSource2.PlayOneShot(clip);
    }

    public void playSwingSound()
    {
        AudioClip clip = swingSounds[UnityEngine.Random.Range(0, damageSounds.Length)];
        audioSource2.PlayOneShot(clip);
    }

    public void playPlayerFootstepSound()
    {
        AudioClip clip = stepSounds[UnityEngine.Random.Range(0, stepSounds.Length)];
        audioSource2.PlayOneShot(clip);
    }

    public void playLandingSound()
    {
        AudioClip clip = stepSounds[UnityEngine.Random.Range(0, landSounds.Length)];
        audioSource2.PlayOneShot(clip);
    }

}

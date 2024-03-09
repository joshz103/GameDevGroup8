using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] AudioClip[] damageSounds;
    [SerializeField] AudioClip critSound;

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
        AudioClip clip = damageSounds[UnityEngine.Random.Range(0, damageSounds.Length)];
        audioSource1.pitch = (Random.Range(0.8f,1.2f));
        audioSource1.PlayOneShot(clip);
    }

    public void playCritSound()
    {
        AudioClip clip = critSound;
        audioSource2.PlayOneShot(clip);
    }

}

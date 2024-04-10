using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour
{
    private playerstats stats;
    [SerializeField] AudioClip pickupSound;
    private AudioSource audioSource1;

    // Start is called before the first frame update
    void Start()
    {
        audioSource1 = GetComponent<AudioSource>();
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerstats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player healed");
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
            stats.addHealth(stats.getMaxHealth());
            Destroy(gameObject, 1f);

            AudioClip clip = pickupSound;
            audioSource1.pitch = (Random.Range(0.95f, 1.05f));
            audioSource1.PlayOneShot(clip);
        }
    }
}
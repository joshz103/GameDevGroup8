using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float velocity;
    public Transform firingPoint;
    public float damage;
    public playerstats stats;
    public GameObject destructionEffect;
    // Start is called before the first frame update

    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerstats>();
        Destroy(gameObject, 5f);
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stats.damage(damage);
        }

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        Destroy(gameObject, 1f);

        if(destructionEffect != null)
        {
            Instantiate(destructionEffect, transform.position, transform.rotation);
        }


    }





}

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
    // Start is called before the first frame update

    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerstats>();

    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stats.damage(damage);
        }

        Destroy(gameObject);
    }



}

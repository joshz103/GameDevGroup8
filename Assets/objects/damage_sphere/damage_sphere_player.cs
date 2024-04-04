using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class damage_sphere_player : MonoBehaviour
{
    private playerstats stats;
    public int damageAmt;
    public bool destroyAfterTime = true;
    public float destroyTime = 0.1f;

    public bool useEnemyPositionInstead;
    public GameObject enemy;

    public bool causesKnockback;
    public float knockbackHeight;
    public float knockbackDistance;

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerstats>();
        if (destroyAfterTime)
        {
            Destroy(gameObject, destroyTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            stats.damage(damageAmt);
            Debug.Log("Damaged a " + other);
            //Instantiate(damagePopupPrefab);
            //soundPlayer.playDamageSound();
            //Debug.Log("Player dealt " + damageMult + " damage! | RNG Roll was " + rng);

            if (causesKnockback)
            {
                if (!useEnemyPositionInstead)
                {
                    other.GetComponent<PlayerController>().playerKnockback(knockbackHeight, knockbackDistance, enemy);
                }
                else
                {
                    other.GetComponent<PlayerController>().playerKnockback(knockbackHeight, knockbackDistance, gameObject);
                }
                
                
            }

        }
    }



}
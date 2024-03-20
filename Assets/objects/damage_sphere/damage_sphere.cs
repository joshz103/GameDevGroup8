using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class damage_sphere : MonoBehaviour
{
    private playerstats stats;
    public int damageAmt;

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerstats>();
        Destroy(gameObject, 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("Enemy") && other.GetComponent<EnemyBehavior>().isDead() == false)
        {
            other.GetComponent<EnemyHP>().damage(damageAmt, false);
            Debug.Log("Damaged a " + other);
            //Instantiate(damagePopupPrefab);
            //soundPlayer.playDamageSound();
            //Debug.Log("Player dealt " + damageMult + " damage! | RNG Roll was " + rng);
        }
    }



}
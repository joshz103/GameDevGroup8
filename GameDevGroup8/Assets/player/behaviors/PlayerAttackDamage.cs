using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerAttackDamage : MonoBehaviour
{
    //private PlayerController playerController;
    public Animator animator;
    public GameObject hitbox;
    private playerstats stats;
    private PlayerController playerController;

    private float playerDamage;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerstats>();
    }

    void Update()
    {
        playerDamage = stats.getOffense();
    }

    IEnumerator removeHitbox()
    {
        yield return new WaitForSeconds(0.1f);
        hitbox.GetComponent<BoxCollider>().enabled = false;
    }

    public void enableHitbox()
    {
        hitbox.GetComponent<BoxCollider>().enabled = true;
        StartCoroutine(removeHitbox());
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHP>().damage(playerDamage);
            Debug.Log("Player dealt " + playerDamage + " damage!");
        }
    }



}

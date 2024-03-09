using System;
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
    private PlayerSounds soundPlayer;

    public GameObject damagePopupPrefab;

    //TestSpawnText damagePopup;
    
    private float damageMult;
    private float recentDMG;

    private float playerDamage;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerstats>();
        soundPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSounds>();
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
        damageMult = playerDamage;
        float rng = UnityEngine.Random.Range(0f, 100f);

        if (other.gameObject.CompareTag("Enemy") && other.GetComponent<EnemyBehavior>().isDead() == false)
        {
            if (stats.getLuck() > rng)
            {
                damageMult *= 2f;
                other.GetComponent<EnemyHP>().damage(damageMult, true);
                soundPlayer.playCritSound();
                recentDMG = damageMult;
                Debug.Log("Critical Hit!");
            }
            else
            {
                recentDMG = damageMult;
                other.GetComponent<EnemyHP>().damage(damageMult, false);
            }
            Instantiate(damagePopupPrefab);
            soundPlayer.playDamageSound();
            Debug.Log("Player dealt " + damageMult + " damage! | RNG Roll was " + rng);
        }
    }

    public string getRecentDamageStr()
    {
        return string.Format("{0:N2}", recentDMG);
    }

}

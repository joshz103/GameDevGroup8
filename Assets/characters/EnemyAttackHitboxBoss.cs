using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHitboxBoss : MonoBehaviour
{
    playerstats stats;
    public EnemyBehaviorBoss enemy;
    public GameObject boss;
    public GameObject hitbox;

    public bool causesKnockback;
    public float knockbackHeight;
    public float knockbackDistance;

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerstats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            stats.damage(enemy.getAttackDamage());
            if (causesKnockback)
            {
                other.GetComponent<PlayerController>().playerKnockback(knockbackHeight, knockbackDistance, boss);
            }
        }
    }

    public void triggerHitbox()
    {
        StartCoroutine(hitboxDelay());
    }

    IEnumerator hitboxDelay()
    {
        hitbox.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        hitbox.GetComponent<BoxCollider>().enabled = false;
    }



}


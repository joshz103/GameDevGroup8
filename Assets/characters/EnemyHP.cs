using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float hp;
    public GameObject enemy;
    public Animator playerAnimator;
    public PlayerAttackDamage playerAttackHitbox;
    public Animator animator;

    public PlayerSounds soundPlayer;

    public int stunChance;

    public float stunTime;
    //private int attackID = 0;

    // Start is called before the first frame update
    void Start()
    {
        //playerAttackHitbox = GetComponent<PlayerAttackDamage>();
        animator = GetComponent<Animator>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    public void damage(float dmg, bool crit)
    {
        // Hitstun test
        /*
        stunTime = dmg * 0.01f;

        animator.SetBool("isHitStunned", true);
        if(stunTime < 0.1f)
        {
            stunTime = 0.1f;
        }
        else if (stunTime > 0.5f)
        {
            stunTime = 0.5f;
        }

        Debug.Log("Stun time = " + stunTime);

        animator.speed = 0.05f;
        playerAnimator.speed = 0.05f;
        StartCoroutine(hitStunEnd());
        */
        hp -= dmg;
        int randomStun = Random.Range(1, 100);
        if (randomStun < stunChance)
        {
            int randomNumber = Random.Range(1, 3);
            string tmp = "stunned" + randomNumber;
            Debug.Log(tmp);
            animator.Play(tmp);
        }
        
    }

    IEnumerator hitStunEnd()
    {
        yield return new WaitForSeconds(stunTime);
        animator.SetBool("isHitStunned", false);
        animator.speed = 1f;
        playerAnimator.speed = 1f;
    }

    public float getHP()
    {
        return hp;
    }

}

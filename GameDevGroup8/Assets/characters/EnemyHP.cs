using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float hp;
    public GameObject enemy;
    public PlayerAttackDamage playerAttackHitbox;
    public Animator animator;

    public PlayerSounds soundPlayer;
    //private int attackID = 0;

    // Start is called before the first frame update
    void Start()
    {
        //playerAttackHitbox = GetComponent<PlayerAttackDamage>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damage(float dmg, bool crit)
    {
        hp -= dmg;
        int randomStun = Random.Range(1, 100);
        if (randomStun > 50)
        {
            int randomNumber = Random.Range(1, 3);
            string tmp = "stunned" + randomNumber;
            Debug.Log(tmp);
            animator.Play(tmp);
        }
        
    }

    public float getHP()
    {
        return hp;
    }

}

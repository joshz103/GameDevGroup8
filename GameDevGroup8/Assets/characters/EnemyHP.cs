using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float hp;
    public GameObject enemy;
    public PlayerAttackDamage playerAttackHitbox;
    public PlayerSounds soundPlayer;

    //private int attackID = 0;

    // Start is called before the first frame update
    void Start()
    {
        //playerAttackHitbox = GetComponent<PlayerAttackDamage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damage(float dmg)
    {
            hp -= dmg;
            soundPlayer.playDamageSound();
    }

    public float getHP()
    {
        return hp;
    }

}

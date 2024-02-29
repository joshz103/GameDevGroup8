using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private playerstats playerStat;
    private float maxHealth;
    private float health;


    // Start is called before the first frame update
    void Start()
    {
        health += maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        updateMaxHealth();
        preventOverhealth();
    }

    private void preventOverhealth()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void updateMaxHealth()
    {
        maxHealth = playerStat.getMaxHealth();
    }

    private void damage(float damage)
    {
        health += damage;
    }

}

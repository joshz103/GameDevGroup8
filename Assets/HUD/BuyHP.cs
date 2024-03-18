using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyHP: MonoBehaviour
{
    
    private playerstats stats;
    
    //private float currencyMultiplier = 1.5f;

    public TMP_Text buttonText;

    public int upgradeVal = 1;

    public AudioClip successSound;
    public AudioClip failSound;

    public AudioSource audio;

    private void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerstats>();
    }

    void Update()
    {
        float currentCurrency = stats.getMoney();
        float currentHealth = stats.getCurrentHealth();

        int cost = 0;
        if (currentHealth <= 10)
        {
            cost = 5;
        }
        else
        {
            //cost = Mathf.RoundToInt(5 * Mathf.Pow(currencyMultiplier, (currentHealth / 10)));
            cost = cost + upgradeVal + 5;
        }
        updateText(cost);
    }

    public void BuyHealthLevel()
    {
        float currentCurrency = stats.getMoney();
        float currentHealth = stats.getCurrentHealth();

        int cost = 0;
        if (currentHealth == 10){
            cost = 5;
        }else{
            //cost = Mathf.RoundToInt(5 * Mathf.Pow(currencyMultiplier, (currentHealth / 10)));
            cost = cost + upgradeVal + 5;
        }


        // Check if player has enough currency to buy
        if (currentCurrency >= cost)
        {
            // Subtract cost from currency
            stats.subtractMoney(cost);

            // Increase HP
            stats.addMaxHealth(5);
            stats.addHealth(5);
            upgradeVal++;
            Debug.Log("Player purchased a health upgrade!");
            audio.PlayOneShot(successSound, 0.25F);
        }
        else
        {
            Debug.Log("Not enough currency to buy more Health!");
            audio.PlayOneShot(failSound, 0.25F);
        }
    }

    public void updateText(int cost)
    {
        buttonText.text = "Upgrade ($" + cost + ")";
    }
}
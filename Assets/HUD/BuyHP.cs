using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyHP: MonoBehaviour
{
    
    private playerstats stats;
    
    private float currencyMultiplier = 1.5f;

    public TMP_Text buttonText;

    private void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerstats>();
    }

    void Update()
    {
        float currentCurrency = stats.getMoney();
        float currentHealth = stats.getCurrentHealth();

        int cost = 0;
        if (currentHealth == 10)
        {
            cost = 5;
        }
        else
        {
            cost = Mathf.RoundToInt(5 * Mathf.Pow(currencyMultiplier, (currentHealth / 10)));
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
            cost = Mathf.RoundToInt(5 * Mathf.Pow(currencyMultiplier, (currentHealth / 10)));
        }


        // Check if player has enough currency to buy
        if (currentCurrency >= cost)
        {
            // Subtract cost from currency
            stats.subtractMoney(cost);

            // Increase HP
            stats.addMaxHealth(5);
            stats.addHealth(5);

            Debug.Log("Player purchased a health upgrade!");
        }
        else
        {
            Debug.Log("Not enough currency to buy more Health!");
        }
    }

    public void updateText(int cost)
    {
        buttonText.text = "Upgrade ($" + cost + ")";
    }
}
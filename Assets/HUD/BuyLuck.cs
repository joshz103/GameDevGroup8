using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyLuck : MonoBehaviour
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
        float currentLuck = stats.getLuck();

        int cost = 0;
        if (currentLuck == 1)
        {
            cost = 5;
        }
        else
        {
            cost = Mathf.RoundToInt(5 * Mathf.Pow(currencyMultiplier, currentLuck));
        }
        updateText(cost);
    }

    public void BuyLuckLevel()
    {
        float currentCurrency = stats.getMoney();
        float currentLuck = stats.getLuck();

        int cost = 0;
        if (currentLuck == 1){
            cost = 5;
        }else{
            cost = Mathf.RoundToInt(5 * Mathf.Pow(currencyMultiplier, currentLuck));
        }


        // Check if player has enough currency to buy
        if (currentCurrency >= cost)
        {
            // Subtract cost from currency
            stats.subtractMoney(cost);

            // Increase Luck level
            stats.addLuck(1);
            Debug.Log("Player purchased a Luck upgrade!");
        }
        else
        {
            Debug.Log("Not enough currency to buy Luck level!");
        }
    }
    public void updateText(int cost)
    {
        buttonText.text = "Upgrade ($" + cost + ")";
    }
}
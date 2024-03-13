using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySpeed : MonoBehaviour
{
    
    private playerstats stats;
    
    private float currencyMultiplier = 1.5f;

    private void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerstats>();
    }

    public void BuySpeedLevel()
    {
        float currentCurrency = stats.getMoney();
        float currentSpeed = stats.getSpeed();

        int cost = 0;
        if (currentSpeed == 1){
            cost = 5;
        }else{
            cost = Mathf.RoundToInt(5 * Mathf.Pow(currencyMultiplier, currentSpeed));
        }


        // Check if player has enough currency to buy
        if (currentCurrency >= cost)
        {
            // Subtract cost from currency
            stats.subtractMoney(cost);

            // Increase offense level
            stats.addSpeed(0.10f);
        }
        else
        {
            Debug.Log("Not enough currency to buy Speed level!");
        }
    }
}
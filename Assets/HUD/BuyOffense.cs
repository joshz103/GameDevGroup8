using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyOffense : MonoBehaviour
{
    
    private playerstats stats;
    
    private float currencyMultiplier = 1.5f;

    private void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerstats>();
    }

    public void BuyOffenseLevel()
    {
        float currentCurrency = stats.getMoney();
        float currentOffense = stats.getOffense();

        int cost = 0;
        if (currentOffense == 1){
            cost = 5;
        }else{
            cost = Mathf.RoundToInt(5 * Mathf.Pow(currencyMultiplier, currentOffense));
        }


        // Check if player has enough currency to buy
        if (currentCurrency >= cost)
        {
            // Subtract cost from currency
            stats.subtractMoney(cost);

            // Increase offense level
            stats.addOffense(1);
        }
        else
        {
            Debug.Log("Not enough currency to buy offense level!");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuySpeed : MonoBehaviour
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
        float currentSpeed = stats.getSpeed();

        int cost = 0;
        if (currentSpeed == 1)
        {
            cost = 5;
        }
        else
        {
            //cost = Mathf.RoundToInt(5 * Mathf.Pow(currencyMultiplier, (currentSpeed*1.96f)));
            cost = cost + upgradeVal + 5;
        }
        updateText(cost);
    }

    public void BuySpeedLevel()
    {
        float currentCurrency = stats.getMoney();
        float currentSpeed = stats.getSpeed();

        int cost = 0;
        if (currentSpeed == 1){
            cost = 5;
        }else{
            //cost = Mathf.RoundToInt(5 * Mathf.Pow(currencyMultiplier, currentSpeed));
            cost = cost + upgradeVal + 5;
        }


        // Check if player has enough currency to buy
        if (currentCurrency >= cost)
        {
            // Subtract cost from currency
            stats.subtractMoney(cost);

            // Increase offense level
            stats.addSpeed(0.10f);
            upgradeVal++;
            Debug.Log("Player purchased a speed upgrade!");
            audio.PlayOneShot(successSound, 0.25F);
        }
        else
        {
            Debug.Log("Not enough currency to buy Speed level!");
            audio.PlayOneShot(failSound, 0.25F);
        }
    }
    public void updateText(int cost)
    {
        buttonText.text = "Upgrade ($" + cost + ")";
    }
}
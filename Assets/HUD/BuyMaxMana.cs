using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyMaxMana : MonoBehaviour
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
        float currentMaxMana = stats.getMaxMana();

        int cost = 0;
        if (currentMaxMana == 100)
        {
            cost = 5;
        }
        else
        {
            cost = cost + upgradeVal + 5;
        }
        updateText(cost);
    }

    public void BuyMaxManaLevel()
    {

        float currentCurrency = stats.getMoney();
        float currentMaxMana = stats.getMaxMana();

        int cost = 0;
        if (currentMaxMana == 100)
        {
            cost = 5;
        }
        else
        {
            //cost = Mathf.RoundToInt(5 * Mathf.Pow(currencyMultiplier, currentOffense));

            cost = cost + upgradeVal + 5;
        }
        // Check if player has enough currency to buy
        if (currentCurrency >= cost)
        {
            // Subtract cost from currency
            stats.subtractMoney(cost);

            // Increase offense level
            stats.addMaxMana(25);

            upgradeVal++;

            Debug.Log("Player purchased a MaxMana upgrade!");

            //play cash register sound now
            audio.PlayOneShot(successSound, 0.25F);
        }
        else
        {
            Debug.Log("Not enough currency to buy MaxMana level!");
            //play buzzer sound now
            audio.PlayOneShot(failSound, 0.25F);
        }

    }

    public void updateText(int cost)
    {
        buttonText.text = "Upgrade ($" + cost + ")";
    }
}

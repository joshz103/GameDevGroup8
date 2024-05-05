using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveCountdownText : MonoBehaviour
{
    public GameObject waveCountdownText;
    public GameObject enemyWaveSpawner;

    private string nextWaveTxt = "Next Wave in: ";
    private int countdownNum;

    private bool hasSpawned = false;

    public void resetCooldown(int n)
    {
        countdownNum = n;
    }

    public void displayCountdown()
    {
        StartCoroutine(OneSecCooldownUpdateTxt());
    }

    public void resetSpawning()
    {
        hasSpawned = false;
    }

    public void countdownDisrupted()
    {
        StopCoroutine(OneSecCooldownUpdateTxt());
        StartCoroutine(OneSecCooldownUpdateTxt());
    }

    public IEnumerator OneSecCooldownUpdateTxt()
    {
        if (enemyWaveSpawner.GetComponent<EnemyWaves>().getWaveNum() != 21)
        {
            for (int i = countdownNum; i > 0; i--)
            {
                yield return new WaitForSeconds(1f);
                countdownNum--;
                waveCountdownText.GetComponent<TextMeshProUGUI>().text = nextWaveTxt + countdownNum;
            }
            if (hasSpawned == false)
            {
                hasSpawned = true;
                waveCountdownText.GetComponent<TextMeshProUGUI>().text = "";
                enemyWaveSpawner.GetComponent<EnemyWaves>().startWave();
            }
        }
        else
        {
            yield return new WaitForSeconds(0.01f);
            enemyWaveSpawner.GetComponent<EnemyWaves>().startWave();
        }

        
    }


}

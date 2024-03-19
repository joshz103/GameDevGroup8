using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveCounterHUD : MonoBehaviour
{
    public GameObject txtHUD;
    public GameObject enemyWaveSpawner;
    private string waveNum;
    public WaveCountdownText waveCountdownTxtObj;

    // Start is called before the first frame update
    void Start()
    {
        waveNum = enemyWaveSpawner.GetComponent<EnemyWaves>().getWaveNumStr();
        txtHUD.GetComponent<TextMeshProUGUI> ().text = waveNum;
    }

    public void updateWaveCounter()
    {
        waveNum = enemyWaveSpawner.GetComponent<EnemyWaves>().getWaveNumStr();
        txtHUD.GetComponent<TextMeshProUGUI>().text = waveNum;
        Debug.Log("Wave " +  waveNum);
        waveCountdownTxtObj.resetCooldown(enemyWaveSpawner.GetComponent<EnemyWaves>().getWaveCooldown());
        waveCountdownTxtObj.displayCountdown();
    }

}

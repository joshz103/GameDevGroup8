using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyWaves : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] bosses;

    public GameObject[] enemySpawnpoint;

    public int enemiesAlive;

    public int waveNum = 0;

    private int waveCooldown = 8;

    //public GameObject waveCounter;
    public WaveCounterHUD waveCounterHUDObj;
    public WaveCountdownText waveCountdownText;

    /*//////////ENEMY IDS//////////
    Basic Enemies
    0. Skeleton
    1. Volcano Walker
    2. Slime
    3. Reaper Ghost
    4. Skeleton Wizard

    Bosses
    0. Skeleton King

    */
    public void Start()
    {
        //waveCounterHUDObj = GetComponent<WaveCounterHUD>();
        checkEnemiesAlive();
    }

    public int getWaveCooldown()
    {
        return waveCooldown;
    }

    public void checkEnemiesAlive()
    {
        if (enemiesAlive <= 0)
        {
            waveNum++;
            waveCountdownText.resetSpawning();
            waveCounterHUDObj.updateWaveCounter();
            StartCoroutine(startWaveCooldown());
        }
    }

    public void startWave()
    {
        if (waveNum < 10)
        {
            spawnWaveRound1to9();
        }

        if (waveNum == 10)
        {
            spawnBossRound10();
        }

        if (waveNum > 10 && waveNum < 20)
        {
            spawnWaveRound10to19();
        }

    }

    public string getWaveNumStr()
    {
        return waveNum.ToString();
    }

    public void removeEnemyCount()
    {
        enemiesAlive--;
        checkEnemiesAlive();
    }

    public void addEnemyCount()
    {
        enemiesAlive++;
        checkEnemiesAlive();
    }

    public IEnumerator startWaveCooldown()
    {
        for (int i = waveCooldown; i > 0; i--)
        {
            yield return new WaitForSeconds(1f);
        }
        //startWave();
    }


    public void spawnWaveRound1to9()
    {
        int randomNum = Random.Range(0, 4); //Should Equal the amount of wave templates!
        switch (randomNum)
        {
            case 0: 
                Template1();
                break;
            case 1:
                Template2();
                break;
            case 2:
                Template3();
                break;
            case 3:
                Template4();
                break;
        }
    }

    public void spawnBossRound10()
    {
        BossTemplate1();
    }

    public void spawnWaveRound10to19()
    {
        int randomNum = Random.Range(0, 6); //Should Equal the amount of wave templates!
        switch (randomNum)
        {
            case 0:
                Template1();
                Template1();
                break;
            case 1:
                Template2();
                Template1();
                break;
            case 2:
                Template3();
                Template1();
                break;
            case 3:
                Template4();
                Template4();
                Template4();
                break;
            case 4:
                Template5();
                break;
            case 5:
                Template6();
                break;
        }
    }

    public void spawnWaveRound20to29()
    {

    }

    public void spawnWaveRound30to39()
    {

    }

    public void spawnWaveRound40to49()
    {

    }


    //WAVE TEMPLATES

    //1-10
    private void Template1()
    {
        for (int i = 0; i < UnityEngine.Random.Range(3, 6); i++)
        {
            int randomSpawnNum = UnityEngine.Random.Range(0, enemySpawnpoint.Length);
            Instantiate(enemies[0], enemySpawnpoint[randomSpawnNum].transform.position, enemySpawnpoint[randomSpawnNum].transform.rotation);
            enemiesAlive++;
        }
    }

    private void Template2()
    {
        for (int i = 0; i < UnityEngine.Random.Range(1, 3); i++)
        {
            int randomSpawnNum = UnityEngine.Random.Range(0, enemySpawnpoint.Length);
            Instantiate(enemies[0], enemySpawnpoint[randomSpawnNum].transform.position, enemySpawnpoint[randomSpawnNum].transform.rotation);
            enemiesAlive++;
        }
        for (int i = 0; i < 2; i++)
        {
            int randomSpawnNum = UnityEngine.Random.Range(0, enemySpawnpoint.Length);
            Instantiate(enemies[1], enemySpawnpoint[randomSpawnNum].transform.position, enemySpawnpoint[randomSpawnNum].transform.rotation);
            enemiesAlive++;
        }
    }

    private void Template3()
    {
        for (int i = 0; i < UnityEngine.Random.Range(3, 5); i++)
        {
            int randomSpawnNum = UnityEngine.Random.Range(0, enemySpawnpoint.Length);
            Instantiate(enemies[2], enemySpawnpoint[randomSpawnNum].transform.position, enemySpawnpoint[randomSpawnNum].transform.rotation);
            enemiesAlive++;
        }
    }

    private void Template4()
    {
        for (int i = 0; i < 1; i++)
        {
            int randomSpawnNum = UnityEngine.Random.Range(0, enemySpawnpoint.Length);
            Instantiate(enemies[0], enemySpawnpoint[randomSpawnNum].transform.position, enemySpawnpoint[randomSpawnNum].transform.rotation);
            enemiesAlive++;
        }

        for (int i = 0; i < 1; i++)
        {
            int randomSpawnNum = UnityEngine.Random.Range(0, enemySpawnpoint.Length);
            Instantiate(enemies[1], enemySpawnpoint[randomSpawnNum].transform.position, enemySpawnpoint[randomSpawnNum].transform.rotation);
            enemiesAlive++;
        }

        for (int i = 0; i < 1; i++)
        {
            int randomSpawnNum = UnityEngine.Random.Range(0, enemySpawnpoint.Length);
            Instantiate(enemies[2], enemySpawnpoint[randomSpawnNum].transform.position, enemySpawnpoint[randomSpawnNum].transform.rotation);
            enemiesAlive++;
        }
    }

    private void Template5()
    {
        for (int i = 0; i < 2; i++)
        {
            int randomSpawnNum = UnityEngine.Random.Range(0, enemySpawnpoint.Length);
            Instantiate(enemies[0], enemySpawnpoint[randomSpawnNum].transform.position, enemySpawnpoint[randomSpawnNum].transform.rotation);
            enemiesAlive++;
        }

        for (int i = 0; i < 1; i++)
        {
            int randomSpawnNum = UnityEngine.Random.Range(0, enemySpawnpoint.Length);
            Instantiate(enemies[3], enemySpawnpoint[randomSpawnNum].transform.position, enemySpawnpoint[randomSpawnNum].transform.rotation);
            enemiesAlive++;
        }
    }

    private void Template6()
    {
        for (int i = 0; i < 4; i++)
        {
            int randomSpawnNum = UnityEngine.Random.Range(0, enemySpawnpoint.Length);
            Instantiate(enemies[0], enemySpawnpoint[randomSpawnNum].transform.position, enemySpawnpoint[randomSpawnNum].transform.rotation);
            enemiesAlive++;
        }

        for (int i = 0; i < UnityEngine.Random.Range(1, 3); i++)
        {
            int randomSpawnNum = UnityEngine.Random.Range(0, enemySpawnpoint.Length);
            Instantiate(enemies[4], enemySpawnpoint[randomSpawnNum].transform.position, enemySpawnpoint[randomSpawnNum].transform.rotation);
            enemiesAlive++;
        }
    }



    private void BossTemplate1()
    {
        for (int i = 0; i < UnityEngine.Random.Range(3, 5); i++)
        {
            int randomSpawnNum = UnityEngine.Random.Range(0, enemySpawnpoint.Length);
            Instantiate(enemies[0], enemySpawnpoint[randomSpawnNum].transform.position, enemySpawnpoint[randomSpawnNum].transform.rotation);
            enemiesAlive++;
        }
        for (int i = 0; i < 1; i++)
        {
            int randomSpawnNum = UnityEngine.Random.Range(0, enemySpawnpoint.Length);
            Instantiate(bosses[0], enemySpawnpoint[randomSpawnNum].transform.position, enemySpawnpoint[randomSpawnNum].transform.rotation);
            enemiesAlive++;
        }
    }

}

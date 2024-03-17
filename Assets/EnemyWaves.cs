using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaves : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] bosses;

    public GameObject[] enemySpawnpoint;

    public int enemiesAlive;

    public int waveNum = 0;

    /*//////////ENEMY IDS//////////
    0. Skeleton
    1. Volcano Walker


    */

    public void checkEnemiesAlive()
    {
        if (enemiesAlive <= 0)
        {
            waveNum++;

            if (waveNum < 10)
            {
                spawnWaveRound1to9();
            }

            if (waveNum == 10)
            {

            }


        }
    }

    public void removeEnemyCount()
    {
        enemiesAlive--;
        checkEnemiesAlive();
    }


    public void Start()
    {
        checkEnemiesAlive();
    }

    public void spawnWaveRound1to9()
    {
        int randomNum = Random.Range(0, 2); //Should Equal the amount of wave templates!
        switch (randomNum)
        {
            case 0: 
                Template1();
                break;
            case 1:
                Template2();
                break;
        }
    }

    public void spawnWaveRound10to19()
    {

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



}

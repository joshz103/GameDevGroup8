using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class gameoverManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    public GameObject gameOverBackground;
    public TMP_Text enemiesKilledText;
    
    private bool isGameOver = false;
    private playerstats stats;
    
    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerstats>();
        Debug.Log("Game Over Screen: OFF");
    }

    // Update is called once per frame
    void Update()
    {
        float currentHealth = stats.getCurrentHealth();
        if ((currentHealth == 0) && !isGameOver)
        {
            gameOver();
        }
        
    }

    public void gameOver()
    {
        Debug.Log("Game Over Screen: ON");
        isGameOver = true;
        gameOverPanel.SetActive(true);
        gameOverBackground.SetActive(true);
        float enemiesKilled = stats.getEnemiesKilled();
        updateText(enemiesKilled);
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void updateText(float enemiesKilled)
    {
        enemiesKilledText.text = "Enemies Killed: " + enemiesKilled;
    }

    public void Restart()
    {
        Debug.Log("Restarting Scene...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToTitle()
    {
        Debug.Log("Going back to title...");
        SceneManager.LoadScene("MainMenu");
    }
}


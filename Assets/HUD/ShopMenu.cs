using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject shopMenu;
    public GameObject shopMenu2;
    public GameObject playerHUD;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        isPaused = false;
        shopMenu.SetActive(false);
        shopMenu2.SetActive(false);
        playerHUD.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        isPaused = true;
        shopMenu.SetActive(true);
        shopMenu2.SetActive(true);
        playerHUD.SetActive(false);
        Time.timeScale = 0f;
    }








}

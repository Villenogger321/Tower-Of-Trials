using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    [Header("References")]

    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject inventoryMenuUI;
    public GameObject player;
    public PlayerInput playerInput;

    [Header("Navigation Buttons")]

    public Slider primarySettingsButton;
    public Button primaryMenuButton;
    public Button primaryInventoryButton;

    void Update()
    {
        if (gameIsPaused)
        {
            playerInput.enabled = false;
            Time.timeScale = 0f;
        }
        else
        {
            playerInput.enabled = true;
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (!gameIsPaused && Input.GetKeyDown(KeyCode.Tab))
        {
            OpenInventory();
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Resume();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        inventoryMenuUI.SetActive(false);

        gameIsPaused = false;
    }

    public void Pause()
    {
        primaryMenuButton.Select();

        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        inventoryMenuUI.SetActive(false);

        gameIsPaused = true;
    }

    public void OpenInventory()
    {
        primaryInventoryButton.Select();

        pauseMenuUI.SetActive(false);
        inventoryMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);

        gameIsPaused = true;
    }

    public void OpenSettings()
    {
        primarySettingsButton.Select();

        pauseMenuUI.SetActive(false);
        inventoryMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);

        gameIsPaused = true;
    }

    void Restart()
    {
        //restart scene
    }

    void QuitGame()
    {
        //load scene "title screen"
    }
}

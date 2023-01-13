using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    static bool gameIsPaused = false;
    bool usingMenu = false;

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

    [Header("Animations")]
    public Animator menuToInventory;
    public GameObject menuToInventoryObj;

    public Animator inventoryToMenu;
    public GameObject inventoryToMenuObj;

    public Animator menuToSettings;
    public GameObject menuToSettingsObj;

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

        if (Input.GetKeyDown(KeyCode.Escape) && !usingMenu)
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
    //-------------------------------------BUTTONS-------------------------------------------------
    public void ResumeButton()
    {
        Resume();
    }
    void Resume()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        inventoryMenuUI.SetActive(false);

        gameIsPaused = false;
    }
    //---------------------------------------------------------------------------------------------
    public async void PauseButton()
    {
        inventoryToMenuObj.SetActive(true);
        inventoryToMenu.SetBool("inventoryToMenu", true);
        usingMenu = true;

        await Task.Delay(380);
        Pause();
    }
    void Pause()
    {
        primaryMenuButton.Select();

        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        inventoryMenuUI.SetActive(false);

        inventoryToMenu.SetBool("inventoryToMenu", false);
        inventoryToMenuObj.SetActive(false);
        usingMenu = false;

        gameIsPaused = true;
    }
    //---------------------------------------------------------------------------------------------
    public async void OpenInventoryButton()
    {
        menuToInventoryObj.SetActive(true);
        menuToInventory.SetBool("menuToInventory", true);
        usingMenu = true;

        await Task.Delay(380);
        OpenInventory();
    }
      void OpenInventory()
    {
        primaryInventoryButton.Select();

        pauseMenuUI.SetActive(false);
        inventoryMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);

        menuToInventory.SetBool("menuToInventory", false);
        menuToInventoryObj.SetActive(false);
        usingMenu = false;

        gameIsPaused = true;
    }
    //---------------------------------------------------------------------------------------------
    public async void OpenSettingsButton()
    {
        menuToSettingsObj.SetActive(true);
        menuToSettings.SetBool("menuToSettings", true);
        usingMenu = true;

        await Task.Delay(380);
        OpenSettings();
    }
    void OpenSettings()
    {
        primarySettingsButton.Select();

        pauseMenuUI.SetActive(false);
        inventoryMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);

        menuToSettings.SetBool("menuToSettings", false);
        menuToSettingsObj.SetActive(false);

        gameIsPaused = true;
    }
    //---------------------------------------------------------------------------------------------
    public void RestartButton()
    {
        Restart();
    }
    void Restart()
    {
        Debug.Log("Restart");
        //restart scene
    }
    //---------------------------------------------------------------------------------------------
    public void QuitGameButton()
    {
        QuitGame();
    }
    void QuitGame()
    {
        Debug.Log("QuitGame");
        //load scene "title screen"
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static bool gameIsPaused = false;

    [Header("References")]

    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject settingsMenuUI;
    [SerializeField] GameObject inventoryMenuUI;
    [SerializeField] GameObject player;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] TrinketInventory trinketInventory;
    public static UIManager Instance;

    [Header("Navigation Buttons")]

    [SerializeField] Slider primarySettingsButton;
    [SerializeField] Button primaryMenuButton;
    [SerializeField] Button primaryInventoryButton;

    void OnSettings()
    {
        gameIsPaused = !gameIsPaused;

        if (gameIsPaused)
            Pause();
        else
            Resume();
    }
    void OnInventory()
    {
        gameIsPaused = !gameIsPaused;

        if (gameIsPaused)
            OpenInventory();
        else
            Resume();
    }

    public void Resume()
    {
        playerInput.enabled = true;

        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        inventoryMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        playerInput.enabled = false;
        primaryMenuButton.Select();

        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        inventoryMenuUI.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void OpenInventory()
    {
        playerInput.enabled = false;
        primaryInventoryButton.Select();

        inventoryMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);

        inventoryUI.UpdateInventory();
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void OpenSettings()
    {
        playerInput.enabled = false;
        primarySettingsButton.Select();

        pauseMenuUI.SetActive(false);
        inventoryMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    void OnInteract()
    {
        trinketInventory.Interact();
    }
    public bool IsPaused()
    {
        return gameIsPaused;
    }

    void Restart()
    {
        //restart scene
    }

    void QuitGame()
    {
        //load scene "title screen"
    }
    void Awake()
    {
        Resume();
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
}

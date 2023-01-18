using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static bool gameIsPaused = false;
    bool usingMenu = false;

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

[Header("Animations")]
    public Animator menuToInventory;
    public GameObject menuToInventoryObj;

    public Animator inventoryToMenu;
    public GameObject inventoryToMenuObj;

    public Animator menuToSettings;
    public GameObject menuToSettingsObj;

    //FMOD
    [Header("FMOD")]
    private FMOD.Studio.EventInstance uiScrollInstance;
    private FMOD.Studio.EventInstance uiPressInstance;
    private FMOD.Studio.EventInstance uiPressStartInstance;
    private FMOD.Studio.EventInstance uiPageFlipInstance;
    private FMOD.Studio.EventInstance uiBookOpeningInstance;
    private FMOD.Studio.EventInstance uiBookClosingInstance;


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
    //-------------------------------------BUTTONS-------------------------------------------------
    public void ResumeButton()
    {
                                                    //button click noise
        Resume();
    }
    public void Resume()
    {
        ////////////////////////// Book closing sfx
        uiBookClosingInstance.start();

        playerInput.enabled = true;

        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        inventoryMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        gameIsPaused = false;
    }
    //---------------------------------------------------------------------------------------------
    public async void PauseButton()
    {
                                                   //button click noise
        inventoryToMenuObj.SetActive(true);
        inventoryToMenu.SetBool("inventoryToMenu", true);
        usingMenu = true;

        ////////////////////////// page turn sfx
        uiPageFlipInstance.start();

        await Task.Delay(380);
        Pause();
    }
    void Pause()
    {
        ////////////////////////// Book opening sfx
        uiBookOpeningInstance.start();

        playerInput.enabled = false;
        //primaryMenuButton.Select();

        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        inventoryMenuUI.SetActive(false);
        Time.timeScale = 0f;

        inventoryToMenu.SetBool("inventoryToMenu", false);
        inventoryToMenuObj.SetActive(false);
        usingMenu = false;

        gameIsPaused = true;
    }
    //---------------------------------------------------------------------------------------------
    public async void OpenInventoryButton()
    {
                                                 //button click noise
        inventoryUI.UpdateInventory();
        menuToInventoryObj.SetActive(true);
        menuToInventory.SetBool("menuToInventory", true);
        usingMenu = true;
        ////////////////////////// page turn sfx
        uiPageFlipInstance.start();

        await Task.Delay(380);
        menuToInventory.SetBool("menuToInventory", false);
        OpenInventory();
    }
      public void OpenInventory()
    {
        playerInput.enabled = false;
        //primaryInventoryButton.Select();

        inventoryMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
		inventoryUI.UpdateInventory();

        menuToInventoryObj.SetActive(false);
        usingMenu = false;

        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    //---------------------------------------------------------------------------------------------
    public async void OpenSettingsButton()
    {
                                                       //button click noise
        menuToSettingsObj.SetActive(true);
        menuToSettings.SetBool("menuToSettings", true);
        usingMenu = true;
        ////////////////////////// page turn sfx
        uiPageFlipInstance.start();

        await Task.Delay(380);
        OpenSettings();
    }
    void OpenSettings()
    {

        playerInput.enabled = false;
       // primarySettingsButton.Select();

        pauseMenuUI.SetActive(false);
        inventoryMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);

        menuToSettings.SetBool("menuToSettings", false);
        menuToSettingsObj.SetActive(false);
		
		Time.timeScale = 0f;
        gameIsPaused = true;
    }
    //---------------------------------------------------------------------------------------------
    public void RestartButton()
    {
                                                      //button click noise
        Restart();
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
        Debug.Log("Restart");
        //restart scene
    }
    //---------------------------------------------------------------------------------------------
    public void QuitGameButton()
    {
                                                     //button click noise
        QuitGame();
    }
    void QuitGame()
    {
        Debug.Log("QuitGame");
        //load scene "title screen"
    }
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
<<<<<<< HEAD

        #region FMOD

        uiScrollInstance = FMODUnity.RuntimeManager.CreateInstance("event:/UI/Scroll");
        uiPressInstance = FMODUnity.RuntimeManager.CreateInstance("event:/UI/Press");
        uiPressStartInstance = FMODUnity.RuntimeManager.CreateInstance("event:/UI/PressStart");
        uiPageFlipInstance = FMODUnity.RuntimeManager.CreateInstance("event:/UI/PageFlip");
        uiBookOpeningInstance = FMODUnity.RuntimeManager.CreateInstance("event:/UI/OpenBook");
        uiBookClosingInstance = FMODUnity.RuntimeManager.CreateInstance("event:/UI/CloseBook");

        #endregion
        //declaring FMOD variabels
=======
        AssignPlayer();
        Resume();
>>>>>>> Ville
    }
    void AssignPlayer()
    { 
        print("assigned");
        player = GameObject.FindGameObjectWithTag("Player");
        playerInput = player.GetComponent<PlayerInput>();
        trinketInventory = player.GetComponent<TrinketInventory>();
    }
}

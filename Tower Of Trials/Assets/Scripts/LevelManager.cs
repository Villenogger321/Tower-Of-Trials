using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public Transform Environment;
    [SerializeField] int floor;
    [Tooltip("A point system to determine how many enemies spawn.\nThe more enemies you kill the higher this score will be.")]
    [SerializeField] int startEnemyPoints;
    public int EnemyPoints;
    
    public List<Spawnpoint> spawnpoints;

    [Header("Generation Variables")]
    [SerializeField] GameObject[] lootSpawnables, obstacleSpawnables;

    public static LevelManager Instance;
    Action levelStart;

    private FMOD.Studio.EventInstance forestMusicInstance;
    private FMOD.Studio.EventInstance forestAmbienceInstance;

    private FMOD.Studio.EventInstance tutorialMusicInstance;
    private FMOD.Studio.EventInstance tutorialAmbienceInstance;

    private FMOD.Studio.EventInstance footstepInstance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {

        //FMOD
        forestMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Forest/Music");
        forestAmbienceInstance = FMODUnity.RuntimeManager.CreateInstance("event:/env/Lvl_1_ambience");
        tutorialMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Tutorial/Music");
        tutorialAmbienceInstance = FMODUnity.RuntimeManager.CreateInstance("event:/env/Lvl_0_ambience");
        footstepInstance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/player/Footsteps");

    }
    public void LoadTutorial()
    {
        levelStart?.Invoke();
        SceneManager.LoadScene(1);

        tutorialMusicInstance.start();
        tutorialAmbienceInstance.start();
        footstepInstance.setParameterByName("Type", 0);
    }
    public void LoadLevel()
    {
        spawnpoints.Clear();
        EnemySpawner.enemySpawner.enemySpawnpoints.Clear();
        PlayerStats.Player.GetComponent<Health>().GiveHealth(1000);
        levelStart?.Invoke();

        SceneManager.LoadScene(Random.Range(2, SceneManager.sceneCountInBuildSettings));

        // spawn everything

        
        //start forest music/ambience
        forestMusicInstance.start();
        forestAmbienceInstance.start();

        footstepInstance.setParameterByName("Type", 1);
    }
    void SaveEnemyPoints()
    {
        startEnemyPoints += EnemyPoints;
    }
    public GameObject GetRandomLootSpawnable()
    {
        return lootSpawnables[Random.Range(0, lootSpawnables.Length)];
    }
    public GameObject GetRandomObstacleSpawnable()
    {
        return obstacleSpawnables[Random.Range(0, obstacleSpawnables.Length)];
    }
    public static void Subscribe(Action _subscribee)
    {
        Instance.levelStart += _subscribee;
    }
    public static void UnSubscribe(Action _subscribee)
    {
        Instance.levelStart -= _subscribee;
    }

    public int GetFloor()
    {
        return floor;
    }
}

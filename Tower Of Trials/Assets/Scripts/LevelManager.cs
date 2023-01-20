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

    }
    public void LoadTutorial()
    {
        levelStart?.Invoke();
        SceneManager.LoadScene(1);
    }
    public void LoadLevel()
    {
        levelStart?.Invoke();

        SceneManager.LoadScene(Random.Range(2, SceneManager.sceneCountInBuildSettings));

        // spawn everything

        //start forest music/ambience
        forestMusicInstance.start();
        forestAmbienceInstance.start();
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

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
    [SerializeField] int enemyPoints;
    

    [Header("Generation Variables")]
    [SerializeField] Spawnpoint[] spawnpoints;
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
        spawnpoints = transform.GetComponentsInChildren<Spawnpoint>();
    }
    void Start()
    {

        //FMOD
        forestMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Forest/Music");
        forestAmbienceInstance = FMODUnity.RuntimeManager.CreateInstance("event:/env/Lvl_1_ambience");

    }
    public void LoadLevel()
    {
        levelStart?.Invoke();

        SceneManager.LoadScene(Random.Range(1, SceneManager.sceneCountInBuildSettings));

        // spawn everything

        //start forest music/ambience
        forestMusicInstance.start();
        forestAmbienceInstance.start();

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

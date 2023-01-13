using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        Environment = GameObject.Find("Environment").transform;
        DontDestroyOnLoad(gameObject);
        spawnpoints = transform.GetComponentsInChildren<Spawnpoint>();
    }
    void Start()
    {
        LoadLevel();
    }
    public void LoadLevel()
    {
        levelStart?.Invoke();
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

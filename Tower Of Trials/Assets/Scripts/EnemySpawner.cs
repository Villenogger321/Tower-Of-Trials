using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemySpawner : MonoBehaviour
{

    Action guardianKilled;
    [SerializeField] GameObject enemy;
    public static EnemySpawner enemySpawner;
    public List<Spawnpoint> enemySpawnpoints;
    bool canSpawn;

    LevelManager levelManager;
    void Start()
    {
        levelManager = LevelManager.Instance;
        LevelManager.Subscribe(SpawnEnemies);
    }
    
    void SpawnEnemies()
    {
        canSpawn = true;

    }

    void Update()
    {
        if (!canSpawn)
            return;
        if (levelManager.EnemyPoints < 100)
        {
            canSpawn = false;
            return;
        }
        if (enemySpawnpoints.Count == 0)
            return;

        int spawnPointInt = Random.Range(0, enemySpawnpoints.Count);
        float radius = enemySpawnpoints[spawnPointInt].radius;



        Vector3 spawnPoint = new Vector3(
            enemySpawnpoints[spawnPointInt].transform.position.x +
            Random.Range(-radius, radius),
            enemySpawnpoints[spawnPointInt].transform.position.y +
            Random.Range(-radius, radius));

        Instantiate(enemy, spawnPoint, Quaternion.identity);

        levelManager.EnemyPoints -= 100;
    }
    void Awake()
    {
        if (enemySpawner != null)
        {
            Destroy(gameObject);
            return;
        }
        enemySpawner = this;
    }
    public static void SubscribeToGuardianKilled(Action _subscribee)
    {
        enemySpawner.guardianKilled += _subscribee;
    }
    public static void UnSubscribeToGuardianKilled(Action _subscribee)
    {
        enemySpawner.guardianKilled -= _subscribee;
    }
}

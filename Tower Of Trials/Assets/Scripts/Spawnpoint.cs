using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawnpoint : MonoBehaviour
{
    public float radius;
    public SpawnType spawnType;

    GameObject spawnObject;
    [SerializeField] GameObject spawnedObject;
    [Range(0, 100)]
    [SerializeField] int spawnChance = 50;

    LevelManager levelManager;
    
    void Spawn()
    {
        if (Random.Range(0, 100) > spawnChance)
            return;
        spawnedObject = Instantiate(spawnObject, transform.position, Quaternion.identity, levelManager.Environment.GetChild(2));
    
                
    }
    void SpawnSetup()
    {
        switch (spawnType)
        {
            case SpawnType.enemy:
                // select enemy to spawn
                spawnChance = 100;
                Spawn();
                break;

            case SpawnType.loot:
                spawnObject = levelManager.GetRandomLootSpawnable();
                radius = 0;
                Spawn();
                break;

            case SpawnType.obstacle:
                spawnObject = levelManager.GetRandomObstacleSpawnable();
                radius = 0;
                Spawn();
                break;

        }
    }
    
    void Start()
    {
        LevelManager.Subscribe(SpawnSetup);
        levelManager = LevelManager.Instance;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
public enum SpawnType
{
    enemy,
    loot,
    obstacle
}

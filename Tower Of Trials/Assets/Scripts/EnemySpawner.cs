using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    Action guardianKilled;
    static EnemySpawner enemySpawner;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void Awake()
    {
        if (enemySpawner != null)
        {
            Destroy(gameObject);
            return;
        }
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

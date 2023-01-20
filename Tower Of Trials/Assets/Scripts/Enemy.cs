using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Health health;
    [SerializeField] int spawnCost, killReward;
    void Start()
    {
        health = GetComponent<Health>();
        health.SubscribeToDeath(AddPointsToManager);
    }

    void AddPointsToManager()
    {
        LevelManager.Instance.EnemyPoints += killReward;
    }
}

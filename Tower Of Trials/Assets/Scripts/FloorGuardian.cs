using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGuardian : MonoBehaviour
{
    void Start()
    {
        GetComponent<Health>().SubscribeToDeath(FloorGuardianDead);
    }
    void FloorGuardianDead()
    {
        print("dead floor guardian");
        EnemySpawner.enemySpawner.guardianKilled?.Invoke();
    }
}

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
        EnemySpawner.enemySpawner.guardianKilled?.Invoke();
    }

    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class SpawnItemOnDeath : MonoBehaviour
{
    Health health;
    void Start()
    {
        health = GetComponent<Health>();
        health.SubscribeToDeath(SpawnItem);
    }

    void SpawnItem()
    {
        ////////////////////////// crate breaking sfx
        // spawn item
    }
}

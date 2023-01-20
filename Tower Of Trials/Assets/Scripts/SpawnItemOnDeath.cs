using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class SpawnItemOnDeath : MonoBehaviour
{
    Health health;


    private FMOD.Studio.EventInstance crateBreakInstance;

    void Start()
    {
        health = GetComponent<Health>();
        health.SubscribeToDeath(SpawnItem);

        crateBreakInstance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/env/CrateBreak");
    }

    void SpawnItem()
    {
        ////////////////////////// crate breaking sfx
        crateBreakInstance.start();

        if (Random.Range(0, 1) == 1)
            Destroy(gameObject);

        TrinketManager.Instance.SpawnTrinket(transform.position);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    private FMOD.Studio.EventInstance goblinAttackHitInstance;

    void Start()
    {

        goblinAttackHitInstance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/enemies/GoblinAttackHit");
    }

    void Update()
    {
        
    }
    void Event_Attack()
    {
        goblinAttackHitInstance.start();
    }
    bool PlayerInRange()
    {
        return true;
    }
}

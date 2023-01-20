using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    private FMOD.Studio.EventInstance goblinAttackHitInstance;
    [SerializeField] int damage;
    EnemyMovement enemyMovement;

    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        goblinAttackHitInstance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/enemies/GoblinAttackHit");
    }

    void Update()
    {
        
    }
    void Event_Attack()
    {
        if (Vector2.Distance(
            transform.position,
            PlayerStats.Player.position) > enemyMovement.GetAttackRange())
            return;

        goblinAttackHitInstance.start();
        Health health = PlayerStats.Player.GetComponent<Health>();

        health.TakeDamage(damage);
        

    }
    bool PlayerInRange()
    {
        return true;
    }
}

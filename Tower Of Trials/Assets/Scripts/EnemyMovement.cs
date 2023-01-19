using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float movementSpeed;
    [SerializeField] float detectionRadius, stopRadius, attackRadius;
    [SerializeField] EnemyAIState state;
    [SerializeField] float attackCooldown;
    float attackTimer;

    [Header("Wandering")]
    float curTimer;
    [SerializeField] float minWaitingTime, maxWaitingTime;
    [SerializeField] float wanderingRadius;
    Vector2 startPos, wanderingDestination;

    Animator anim;
    Transform player;
    void Start()
    {
        player = PlayerStats.Player;
        startPos = transform.position;
    }
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        switch (state)
        {
            case EnemyAIState.idle:
                HandleIdleState();
                CheckForAggroToPlayer();
                break;
            case EnemyAIState.wandering:
                HandleWanderingState();
                WalkTowards(wanderingDestination);
                CheckForAggroToPlayer();
                break;
            case EnemyAIState.chasing:
                HandleChasingState();
                break;
            case EnemyAIState.attacking:
                HandleAttackingState();
                break;
        }
        attackTimer -= Time.deltaTime;
    }
    void HandleIdleState()
    {
        curTimer -= Time.deltaTime;
        anim.SetBool("Walking", false);

        if (curTimer <= 0)
        {
            state = EnemyAIState.wandering;
            curTimer = Random.Range(minWaitingTime, maxWaitingTime);
            wanderingDestination = startPos + GetWanderingDestination();
        }
    }
    void HandleWanderingState()
    {
        if (Vector2.Distance(transform.position, wanderingDestination) <= 0.1f)
            state = EnemyAIState.idle;

        Vector2 wanderingDir = wanderingDestination - (Vector2)transform.position;
        anim.SetFloat("Horizontal", wanderingDir.x);
        anim.SetFloat("Vertical", wanderingDir.y);
    }
    void HandleChasingState()
    {
        if (Vector2.Distance(transform.position, player.position) < attackRadius)
        {
            state = EnemyAIState.attacking;

            if (Vector2.Distance(transform.position, player.position) < stopRadius)
            {
                if (attackTimer > 0)
                    state = EnemyAIState.idle;
                return;
            }
        }
        

        WalkTowards(player.position);

        Vector2 playerDir = player.position - transform.position;
        anim.SetFloat("Horizontal", playerDir.x);
        anim.SetFloat("Vertical", playerDir.y);
    }
    void HandleAttackingState()
    {
        // start anim
        if (attackTimer > 0)
        {
            state = EnemyAIState.chasing;
            return;
        }

        attackTimer = attackCooldown;
        anim.SetBool("Walking", false);
        anim.SetBool("Attacking", true);
    }
    void CheckForAggroToPlayer()
    {
        if (Vector2.Distance(transform.position, player.position) < detectionRadius)
        {
            state = EnemyAIState.chasing;
        }
    }
    void WalkTowards(Vector3 _destination)
    {
        ////////////////////////// enemy walking sfx

        anim.SetBool("Walking", true);
        transform.position += movementSpeed * Time.deltaTime * (_destination - transform.position).normalized;
    }
    Vector2 GetWanderingDestination()
    {
        Vector2 destination = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        destination.Normalize();

        return Random.Range(0, wanderingRadius) * destination;
    }
    void Event_AttackDone()
    {
        state = EnemyAIState.idle;
        anim.SetBool("Attacking", false);
    }
    enum EnemyAIState
    {
        idle,
        wandering,
        chasing,
        attacking
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(Application.isPlaying?startPos:transform.position, wanderingRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(
            transform.position.x,
            transform.position.y
            + 0.5f), attackRadius);
    }
}

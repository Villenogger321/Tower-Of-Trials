using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float movementSpeed;
    [SerializeField] float detectionRadius, attackRadius;
    [SerializeField] float wanderingRadius;
    [SerializeField] EnemyAIState state;

    [SerializeField] float curTimer, minWaitingTime, maxWaitingTime;
    Vector2 startPos, wanderingDestination;
    Animator anim;
    Transform player;


    private FMOD.Studio.EventInstance goblinWalkInstance;
    private FMOD.Studio.EventInstance goblinIdleInstance;
    private FMOD.Studio.EventInstance goblinAttackInstance;

    void Start()
    {
        player = PlayerStats.Player;
        startPos = transform.position;

        goblinWalkInstance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/enemies/GoblinSteps");
        goblinAttackInstance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/enemies/GoblinAttack");
        goblinIdleInstance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/enemies/GoblinTalk");
    }
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        FMODUnity.RuntimeManager.AttachInstanceToGameObject(goblinWalkInstance, GetComponent<Transform>(), GetComponent<Rigidbody2D>()); //FMOD

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
                WalkTowards(player.position);
                break;
            case EnemyAIState.attacking:
                HandleAttackingState();
                break;
        }
    }
    void HandleIdleState()
    {
        //idle sound(s)
        goblinIdleInstance.start();

        curTimer -= Time.deltaTime;
        anim.SetBool("Walking", false);

        goblinWalkInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

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
        //"chasing" , stop idle sound
        goblinIdleInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        if (Vector2.Distance(transform.position, player.position) < attackRadius)
        {
            state = EnemyAIState.attacking;
        }
        Vector2 playerDir = player.position - transform.position;
        anim.SetFloat("Horizontal", playerDir.x);
        anim.SetFloat("Vertical", playerDir.y);
    }
    void HandleAttackingState()
    {
        //attack sfx
        goblinAttackInstance.start();
        // start anim
        anim.SetBool("Walking", false);
        anim.SetBool("Attacking", true);

        goblinWalkInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
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
        goblinWalkInstance.start();

        anim.SetBool("Walking", true);
        transform.position += movementSpeed * Time.deltaTime * (_destination - transform.position).normalized;
    }
    Vector2 GetWanderingDestination()
    {
        Vector2 destination = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        destination.Normalize();

        return Random.Range(0, wanderingRadius) * destination;
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
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float colliderDistance;
    [SerializeField] LayerMask hitMask;

    Animator anim;
    Vector2 movement;
    Rigidbody2D rb;


    private FMOD.Studio.EventInstance footstepInstance;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        footstepInstance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/player/Footsteps");
    }

    void Start()
    {
        footstepInstance.start();
        LevelManager.Subscribe(StopFootsteps);
    }

    void Update()
    {
        Movement();
        //Debug.Log(movement);
        //Debug.Log(Mathf.Abs(movement.x) + Mathf.Abs(movement.y));

        footstepInstance.setParameterByName("isMoving", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));
    }
    void StopFootsteps()
    {
        footstepInstance.setParameterByName("isMoving", 0);
        footstepInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    void Movement()
    {
        transform.position += movementSpeed * Time.deltaTime * (Vector3)movement;
    }
    void OnMove(InputValue value)
    {
        ////////////////////////// walking sfx (think it should be here)

        
        movement = value.Get<Vector2>();

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);
    }
}

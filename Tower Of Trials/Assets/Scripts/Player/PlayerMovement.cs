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

    [Header("FMOD")]
    private FMOD.Studio.EventInstance footstepInstance; //FMOD instance for calling the Footstep event


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        #region FMOD
        //FMOD creating an instance variable for the event that can be called
        footstepInstance = FMODUnity.RuntimeManager.CreateInstance("event:/echo/SFX/Footsteps");
        #endregion
    }
    void Update()
    {
        Movement();
        Debug.Log(movement);
    }
    void Movement()
    {
        transform.position += movementSpeed * Time.deltaTime * (Vector3)movement;


        /*Vector2 currentPos = rb.position;
        Vector2 adjustedMovement = movement * movementSpeed;
        Vector2 newPos = currentPos + adjustedMovement * Time.fixedDeltaTime;

        Vector2 curPos = new Vector2(transform.position.x, transform.position.y);

        Vector2 direction = newPos - curPos;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, colliderDistance, hitMask);

        rb.velocity = newPos;*/

    }
    void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
        //rb.velocity = value.Get<Vector2>() * movementSpeed;

        #region FMOD
        //trigger FMOD footsteps sound loop

        if(movement.y != 0 || movement.x != 0)//if moving
        {
            footstepInstance.start();
        }
        if(movement.x == 0 && movement.y == 0)//if not moving
        {
            footstepInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        #endregion

    }
}

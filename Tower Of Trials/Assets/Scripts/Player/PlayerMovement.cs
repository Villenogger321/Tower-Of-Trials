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

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        Movement();
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

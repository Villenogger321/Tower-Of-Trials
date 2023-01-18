using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private int speed = 4;
    //[SerializeField] private int running = 8;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Action<Vector2> updateMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
        updateMove?.Invoke(movement);  
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    public void SubscribeToMoveUpdate(Action<Vector2> moveListener)
    {
         updateMove += moveListener;
    }
    public void UnsubscribeToMoveUpdate(Action<Vector2> moveListener)
    {
        updateMove -= moveListener;
    }
}
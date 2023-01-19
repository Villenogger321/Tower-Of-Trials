using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Parallax : MonoBehaviour
{
    float lengthX, lengthY, startPos;
    Transform camera;
    public float parallaxEffect;
    Vector3 worldPos;

    [SerializeField] Vector2 distClamp;

    void Start()
    {
        camera = Camera.main.transform;
        startPos = transform.position.x;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
    }
    
    void Update()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Camera.main.nearClipPlane;
        worldPos = Camera.main.ScreenToWorldPoint(mousePos);
    }

    void FixedUpdate()
    {
        float distX = worldPos.x * parallaxEffect;
        float distY = worldPos.y * parallaxEffect;


        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(startPos + distY, distClamp.x, distClamp.y),
            transform.position.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrinketPhysics : MonoBehaviour
{
    Vector2 floorPos;

    public void SetFloorPosition(Vector2 _pos)
    {
        floorPos = new Vector2(_pos.x, Random.Range(_pos.y - .25f, _pos.y + .25f));
    }
    void Start()
    {
        SetFloorPosition(GameObject.FindGameObjectWithTag("Player").transform.position);
    }
    void Update()
    {
        if (transform.position.y <= floorPos.y) // trinket hit ground, sound effect?
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            Destroy(this);
        }
    }
}

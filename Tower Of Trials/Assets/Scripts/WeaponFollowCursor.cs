using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponFollowCursor : MonoBehaviour
{
    [SerializeField] float maxDistance = 0.5f;
    Transform WeaponPos;


    void Start()
    {
        WeaponPos = transform.parent;

    }
    void Update()
    {
        Vector3 tempMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos = new Vector3(tempMousePos.x, tempMousePos.y, 0);

        Vector2 mouseDir = mousePos - (Vector2)WeaponPos.position;


        if (mouseDir.magnitude > maxDistance)
            mouseDir = mouseDir.normalized * maxDistance;

        transform.localPosition = mouseDir;
        /*
        if (Vector2.Distance(WeaponPos.position, mousePos) < maxDistance)
            return;
        else
            transform.position = mousePos;
        */
        //Vector3.Lerp(WeaponPos.position, mousePos, maxDistance);


    }
}

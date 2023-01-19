using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;

public class Portal : MonoBehaviour
{
    Animator anim;
    bool isOpen;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        EnemySpawner.SubscribeToGuardianKilled(OpenPortal);
    }
    void OpenPortal()
    {
        anim.SetTrigger("OpenPortal");
        isOpen = true;
    }

    void OnTriggerEnter2D(Collider2D _col)
    {
        if (!isOpen)
            return;

        if (_col.transform.CompareTag("Player"))
            LevelManager.Instance.LoadLevel();
    }
}

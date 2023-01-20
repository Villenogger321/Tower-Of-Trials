using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;

public class Portal : MonoBehaviour
{
    Animator anim;
    bool isOpen;
    [SerializeField] bool startOpen;
    #region FMOD

    private FMOD.Studio.EventInstance portalInstance;

    #endregion

    void Awake()
    {
        anim = GetComponent<Animator>();

        #region FMOD

        portalInstance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/env/Portal");

        #endregion
    }
    void Start()
    {
        EnemySpawner.SubscribeToGuardianKilled(OpenPortal);

        if (startOpen)
            OpenPortal();
    }
    void OpenPortal()
    {
        portalInstance.start(); //FMOD sfx
        anim.SetBool("OpenPortal", true);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    private FMOD.Studio.EventInstance torchLitInstance; //FMOD

    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();

        torchLitInstance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/env/TorchLightingUp"); //FMOD
        
    }

    void Update()
    {

    }

    public void LightTorch()
    {
        anim.SetBool("Lit", true);

        torchLitInstance.start(); //FMOD
    }
}
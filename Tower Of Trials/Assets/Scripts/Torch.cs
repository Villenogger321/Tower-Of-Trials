using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void LightTorch()
    {
        print("lit");
        anim.SetBool("Lit", true);
    }
}

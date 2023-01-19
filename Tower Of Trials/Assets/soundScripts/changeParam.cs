using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeParam : MonoBehaviour
{

    public FMODUnity.EventReference tutMusicEvent;
    private FMOD.Studio.EventInstance tutMusicInstance;

    // Start is called before the first frame update
    void Start()
    {
        tutMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/echo/Music/Tutorial/Music");

        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("hit x");
            tutMusicInstance.start();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("hit p");
            tutMusicInstance.setParameterByName("progress", 1);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("hit l");
            tutMusicInstance.setParameterByName("progress", 0);
        }
    }
}

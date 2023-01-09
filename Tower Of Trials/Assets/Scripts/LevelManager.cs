using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int floor;

    public static LevelManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    void Update()
    {
        
    }
    public int GetFloor()
    {
        return floor;
    }
}

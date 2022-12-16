using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    [SerializeField] private int ticksPerSecond;
    private float tickInterval;
    private float tickTimer;

    private Action tickAction;
    private static TickManager tickManager;
    void Awake()
    {
        if (tickManager != null)
        {
            Debug.LogWarning("Multiple tick managers");
            return;
        }
        tickInterval = 1f / ticksPerSecond;

        tickManager = this;
    }
    void FixedUpdate()
    {
        tickTimer += Time.fixedDeltaTime;

        if (tickTimer >= tickInterval)
        {
            tickTimer -= tickInterval;
            tickAction?.Invoke();
        }
    }
    public static void Subscribe(Action _subscribee)
    {
        tickManager.tickAction += _subscribee;
    }
    public static void UnSubscribe(Action _subscribee)
    {
        tickManager.tickAction -= _subscribee;
    }

}

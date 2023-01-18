using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    [SerializeField] int ticksPerSecond;
    float tickInterval;
    float tickTimer;

    Action tickAction;
    static TickManager tickManager;
    void Awake()
    {
        if (tickManager != null)
        {
            Destroy(gameObject);
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

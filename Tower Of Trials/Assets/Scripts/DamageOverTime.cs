using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime
{
    public DamageCollection DamageCollection;
    public int TicksLeft;
    public DamageOverTime(int _ticks, int _damage, DamageType _type)
    {
        TicksLeft = _ticks;
        DamageCollection = new DamageCollection(_damage, _type);
    }

    public int TotalDamageLeft()
    {
        return DamageCollection.Damage * TicksLeft;
    }
    public bool Tick()
    {
        TicksLeft--;
        return TicksLeft < 1;
    }
}
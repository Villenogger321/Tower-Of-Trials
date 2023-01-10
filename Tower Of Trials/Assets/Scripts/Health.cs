using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int maxHealth = 100;
    public Action onDeath;
    public Action<int, DamageType> onDamage;
    private List<DamageOverTime> damageOverTimeList = new();
    public void TakeDamage(int _damage, DamageType _type = DamageType.physical)
    {
        health -= _damage;
        if (health <= 0)
        {
            onDeath?.Invoke();
            return;
        }
        onDamage?.Invoke(health / maxHealth, _type);
        DisplayWorldText.DisplayText(transform, _damage.ToString(), Color.red);
    }
    public void TakeDamage(DamageCollection[] _damageCollection)
    {
        int totalDamage = 0;
        foreach (var item in _damageCollection)
        {
            totalDamage += item.Damage; // multiply with damage type resistance
            DisplayWorldText.DisplayText(transform, item.Damage.ToString(), Color.red);
        }

        health -= totalDamage;
        if (health <= 0)
        {
            onDeath?.Invoke();
            DisplayWorldText.DisplayText(transform, "L", Color.red);
            return;
        }

        foreach (var item in _damageCollection)
        {
            onDamage?.Invoke(health / maxHealth, item.Type);
        }
    }
    [ContextMenu("damage")]
    private void TempTest()
    {
        ApplyNewDamageOverTime(new DamageOverTime(10, 5, DamageType.fire));
    }
    private void Start()
    {
        health = maxHealth;    
        TickManager.Subscribe(OnTick);  
    }
    private void OnTick()
    {
        DealDamageOverTime();
    }
    private void DealDamageOverTime()
    {
        if (damageOverTimeList.Count <= 0)
            return;

        DamageCollection[] damageCollection = new DamageCollection[damageOverTimeList.Count];
        for (int i = 0; i < damageOverTimeList.Count; i++)
        {
            damageCollection[i] = damageOverTimeList[i].DamageCollection;

            if (damageOverTimeList[i].Tick())
            {
                damageOverTimeList.RemoveAt(i);
            }
        }
        TakeDamage(damageCollection);
    }
    public void ApplyNewDamageOverTime(DamageOverTime _damageOverTime)
    {
        foreach (var _curDmgOverTime in damageOverTimeList)
        {
            if (_curDmgOverTime.DamageCollection.Type == _damageOverTime.DamageCollection.Type)
            {
                if (_curDmgOverTime.TotalDamageLeft() < _damageOverTime.TotalDamageLeft())
                {
                    // new damage over time is greater
                    damageOverTimeList[damageOverTimeList.IndexOf(_curDmgOverTime)] = _damageOverTime;
                }
                return;
            }
        }
        damageOverTimeList.Add(_damageOverTime);
    }
}
public struct DamageCollection
{
    public int Damage;
    public DamageType Type;

    public DamageCollection(int _damage, DamageType _type)
    {
        Damage = _damage;
        Type = _type;
    }
}

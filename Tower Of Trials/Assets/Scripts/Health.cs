using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int maxHealth = 100;
    public Action OnDeath;
    public Action<int, DamageType> OnDamage;
    private List<DamageOverTime> damageOverTimeList = new();
    public void TakeDamage(int _damage, DamageType _type = DamageType.physical)
    {
        health -= _damage;
        if (health <= 0)
        {
            OnDeath?.Invoke();
            return;
        }
        OnDamage?.Invoke(health / maxHealth, _type);
        DisplayWorldText.DisplayText(transform, _damage.ToString(), Color.red);
    }
    public void TakeDamage(DamageCollection[] _damageCollection)
    {
        ////////////////////////// take damage sfx

        int totalDamage = 0;
        foreach (var item in _damageCollection)
        {
            totalDamage += item.Damage; // multiply with damage type resistance
            DisplayWorldText.DisplayText(transform, item.Damage.ToString(), Color.red);
        }
        if (transform.CompareTag("Player"))
            PlayerTakeDamage(totalDamage);

        if (transform.CompareTag("Enemy"))
            EnemyTakeDamage(totalDamage);

        health -= totalDamage;
        if (health <= 0)
        {
            OnDeath?.Invoke();
            DisplayWorldText.DisplayText(transform, "L", Color.red);
            return;
        }

        foreach (var item in _damageCollection)
        {
            OnDamage?.Invoke(health / maxHealth, item.Type);
        }
    }
    public void SubscribeToDeath(Action _subscribee)
    {
        OnDeath += _subscribee;
    }
    public void UnSubscribeFromDeath(Action _subscribee)
    {
        OnDeath -= _subscribee;
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
    void OnDisable()
    {
        TickManager.UnSubscribe(OnTick);
    }

    void PlayerTakeDamage(float _totalDamage)
    {
        // player damage sound
        if (health - _totalDamage <= 0)
        {
            // player death sound
        }
        // if you don't want damage & death sound to stack when dying you'll just
        // write else here :-)
    }
    void EnemyTakeDamage(float _totalDamage)
    {
        // enemy damage sound
        if (health - _totalDamage <= 0)
        {
            // enemy death sound
        }
        // if you don't want damage & death sound to stack when dying you'll just
        // write else here :-)
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

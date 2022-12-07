using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    int health = 20;
    int maxHealth = 20;
    public Action onDeath;
    public Action<float, DamageType> onDamage;
    public void TakeDamage(int _damage, DamageType _type = DamageType.physical)
    {
        health -= _damage;
        if (health <= 0)
        {
            onDeath?.Invoke();
            return;
        }
        onDamage?.Invoke(health / maxHealth, _type);
    }
}

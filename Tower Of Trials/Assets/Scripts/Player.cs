using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int health;
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int _damage)
    {
        health -= _damage;

        if (health < 0)
            Die();
    }
    public void AddHealth(int _health)
    {
        health += _health;

        if (health > maxHealth)
            health = maxHealth;
    }

    public int GetHealth()
    {
        return health;
    }

    void Die()
    {
        health = 0;
    }
}

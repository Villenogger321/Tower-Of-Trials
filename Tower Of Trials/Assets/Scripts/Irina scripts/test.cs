using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    public int damageN = 1;
    public int healN = 1;
    private bool dead = false;

    public HealthBar healthBar;
    public GameObject blood;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(damageN);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeHeal(healN);
        }
        
        if (currentHealth == 0)
        {
            blood.SetActive(false);
            dead = true;
        }

        if (dead == true)
        {
            this.enabled = false;
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void TakeHeal(int heal)
    {
        currentHealth += heal;
        healthBar.SetHealth(currentHealth);
    }

    
}

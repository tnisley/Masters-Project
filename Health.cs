using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to keep track of health

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int initialHealth;
    public int currentHealth;

    private void Awake()
    {
        currentHealth = initialHealth;
    }

    public void Heal(int recoverAmt)
    {
        currentHealth += recoverAmt;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public void Damage(int dmgAmt)
    {
        currentHealth -= dmgAmt;
        if (currentHealth < 0)
            currentHealth = 0;
    }

    public bool IsDead()
    {
        return currentHealth == 0;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

}

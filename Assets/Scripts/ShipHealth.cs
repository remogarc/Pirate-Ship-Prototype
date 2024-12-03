using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(float damageAmount)
    {
        currentHealth -= damageAmount;

        // Ensure health does not go below zero
        currentHealth = Mathf.Max(currentHealth, 0f);

        if (currentHealth <= 0f)
        {
            Debug.Log("Ship has been destroyed!");
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log("Health: " + currentHealth);
    }
}


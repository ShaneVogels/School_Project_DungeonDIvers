using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Public variables to set in the Unity Editor
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    // Event for when the player dies
    public delegate void PlayerDeath();
    // public event PlayerDeath OnPlayerDeath;

    void Start()
    {
        // Initialize current health to max health
        currentHealth = maxHealth;

        // Update the health bar UI
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    // Method to handle taking damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Ensure health does not drop below zero
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        // Update the health bar UI
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        // Check if the player is dead
        if (currentHealth == 0)
        {
            Die();
        }
    }

    // Method to handle healing
    public void Heal(int amount)
    {
        currentHealth += amount;

        // Ensure health does not exceed max health
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // Update the health bar UI
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
    }

    // Method to handle player death
    void Die()
    {
        // Invoke the player death event
        // OnPlayerDeath?.Invoke();
        
        Destroy(gameObject);
    }
}

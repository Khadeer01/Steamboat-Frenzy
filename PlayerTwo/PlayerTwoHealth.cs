using UnityEngine;

public class PlayerTwoHealth : MonoBehaviour
{
    public int maxHealth = 200; // Maximum health
    public int currentHealth; // Current health

    void Start()
    {
        currentHealth = maxHealth; // Initialize health to max
    }

    // Apply damage to the ship
    public void TakeDamage(int damageAmount)
    {
       

        currentHealth -= damageAmount; // Subtract damage
        Debug.Log("Player Two Health: " + currentHealth);

        if (currentHealth <= 0) // Check if the ship is destroyed
        {
            currentHealth = 0;
            DestroyShip(); // Handle destruction
        }
    }

    private void DestroyShip()
    {
        Debug.Log("Player Two's Ship Destroyed!");
        Destroy(gameObject); // Remove the ship
    }
}
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    public int maxHealth = 200; // Maximum health
    public int currentHealth; // Current health
    public bool isPlayerAlive = true;

    void Start()
    {
        currentHealth = maxHealth; // Initialize health to max
    }

    // Apply damage to the ship
    public void TakeDamage(int damageAmount)
    {
        if (!isPlayerAlive) return; // Don't apply damage if the player is already destroyed

        currentHealth -= damageAmount; // Subtract damage
        Debug.Log("Player One Health: " + currentHealth);

        if (currentHealth <= 0) // Check if the ship is destroyed
        {
            currentHealth = 0;
            DestroyShip(); // Handle destruction
        }
    }

    private void DestroyShip()
    {
        isPlayerAlive = false;
        Debug.Log("Player One's Ship Destroyed!");
        Destroy(gameObject); // Remove the ship
    }
}
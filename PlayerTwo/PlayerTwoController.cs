using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoController : MonoBehaviour
{
    public float speed = 5f; // Speed of the ship
    public float rotationSpeed = 100f; // Speed of manual rotation
    public bool moveForward = false; // Control variable for forward movement
    public bool moveLeft = false; // Control variable for leftward movement
    public bool isRotatingWithPotentiometer = true; // Toggle for potentiometer rotation control
    public GameObject projectilePrefab; // Projectile prefab to fire (assign in the Inspector)
    public Transform firePoint; // Point from which the projectile is fired (assign in the Inspector)
    public bool canFire = false; // Control variable for firing

    private int currentBullets; // Current number of bullets available
    public int maxBullets = 10; // Maximum number of bullets that the player can hold
    public KeyCode reloadKey = KeyCode.O; // Key to reload the bullets (for Player 2, use 'O')

    private float targetRotation = 0f; // Default potentiometer value (no rotation)
    public float turnSpeed = 200f; // Smooth turn speed for potentiometer rotation

    void Start()
    {
        // Initialize the current bullets count
        currentBullets = maxBullets;
    }

    void Update()
    {
        // Manual rotation with I and K
        if (Input.GetKey(KeyCode.I))
        {
            isRotatingWithPotentiometer = false; // Disable potentiometer control when I/K is pressed
            transform.Rotate(Vector3.forward * -rotationSpeed * Time.deltaTime); // Rotate counterclockwise
        }
        else if (Input.GetKey(KeyCode.K))
        {
            isRotatingWithPotentiometer = false; // Disable potentiometer control when I/K is pressed
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime); // Rotate clockwise
        }
        else
        {
            // If no manual input, allow potentiometer control
            isRotatingWithPotentiometer = true;
        }

        // Rotate ship using potentiometer (only if potentiometer control is active)
        if (isRotatingWithPotentiometer)
        {
            RotateShipWithPotentiometer();
        }

        // Ship movement logic
        if (Input.GetKey(KeyCode.L) || moveForward)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.Self); // Move forward in local space
        }

        if (Input.GetKey(KeyCode.J) || moveLeft)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self); // Move left in local space
        }

        // Fire a projectile when 'P' is pressed and there are bullets
        if ((Input.GetKeyDown(KeyCode.P) && currentBullets > 0) || canFire)
        {
            FireProjectile();
            currentBullets--; // Decrease the number of bullets after firing
            canFire = false; // Reset firing after firing once
        }

        // Reload when the reload key is pressed
        if (Input.GetKeyDown(reloadKey))
        {
            ReloadBullets();
        }
    }

    // Method to fire a projectile
    void FireProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }

    // Method to set rotation based on potentiometer value
    public void SetRotation(float rotationValue)
    {
        targetRotation = rotationValue; // Update the target rotation from potentiometer
    }

    // Perform the rotation of the ship based on potentiometer input
    void RotateShipWithPotentiometer()
    {
        // Map the potentiometer value (0-1024) to a rotation range (-135 to 135 degrees)
        float mappedRotation = Mathf.Lerp(-135f, 135f, Mathf.InverseLerp(0f, 1024f, targetRotation));

        // Smoothly rotate the ship towards the mapped rotation
        Quaternion targetQuaternion = Quaternion.Euler(0, 0, mappedRotation);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetQuaternion, turnSpeed * Time.deltaTime);
    }

    // Method to reload the bullets to the max value
    public void ReloadBullets()
    {
        currentBullets = maxBullets; // Set the number of bullets back to maxBullets
        Debug.Log("Reloading... Bullets: " + currentBullets);
        AudioSource sound = GetComponent<AudioSource>();
        if (sound != null && sound.clip != null)
        {
            sound.Play();
        }
       
    }

    // Method to check if there are bullets left
    public bool HasBullets()
    {
        return currentBullets > 0;
    }
}

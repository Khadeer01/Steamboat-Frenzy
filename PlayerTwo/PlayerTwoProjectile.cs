using UnityEngine;

public class PlayerTwoProjectile : MonoBehaviour
{
    public float speed = 10f; // Speed of the projectile
    public float lifetime = 3f; // Time before the projectile is destroyed
    public int damage = 10; // Damage dealt by the projectile

    void Start()
    {
        // Schedule the projectile for destruction after `lifetime` seconds
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move the projectile forward
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the projectile hits Player One's ship
        ShipHealth playerOneHealth = collision.gameObject.GetComponent<ShipHealth>();
        if (playerOneHealth != null)
        {
            playerOneHealth.TakeDamage(damage); // Apply damage to Player One
            Destroy(gameObject); // Destroy projectile after collision
        }
    }
}
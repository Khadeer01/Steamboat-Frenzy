using UnityEngine;

public class PlayerOneProjectile : MonoBehaviour
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
        // Check if the projectile hits Player Two's ship
        PlayerTwoHealth playerTwoHealth = collision.gameObject.GetComponent<PlayerTwoHealth>();
        if (playerTwoHealth != null)
        {
            playerTwoHealth.TakeDamage(damage); // Apply damage to Player Two
            Destroy(gameObject); // Destroy projectile after collision
        }
    }
}
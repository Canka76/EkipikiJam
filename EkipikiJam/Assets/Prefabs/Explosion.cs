using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float speed = 20f;           // Speed of the projectile
    public float lifetime = 5f;        // Time before the projectile is destroyed
    private Rigidbody rb;              // Reference to the Rigidbody
    
    public GameObject ExplosionEffect; // Reference to the explosion effect
    public ParticleSystem particleSystem; // Reference to the particle system

    void Start()
    {
        // Ensure Rigidbody is assigned
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Set velocity based on the object's forward direction
            rb.linearVelocity = transform.forward * speed; // Corrected to 'velocity'
        }

        // Destroy the projectile after its lifetime expires
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Instantiate the explosion effect
        Instantiate(ExplosionEffect, transform.position, Quaternion.identity);

        Debug.Log($"Projectile hit {collision.gameObject.name}");

        // Stop the particle system if assigned
        if (particleSystem != null)
        {
            particleSystem.Stop();
        }

        // Destroy the projectile on impact
        gameObject.SetActive(false);
    }
}
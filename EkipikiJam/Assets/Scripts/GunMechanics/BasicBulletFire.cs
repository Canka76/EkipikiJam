using UnityEngine;

public class GunFire : MonoBehaviour
{
    public float speed = 20f;            // Speed of the projectile
    public float lifetime = 5f;         // Time before the projectile is destroyed
    private Rigidbody rb;               // Reference to the Rigidbody

    void Start()
    {
        // Ensure Rigidbody is assigned
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Set velocity based on the object's forward direction
            rb.linearVelocity = transform.forward * speed;
        }

        // Destroy the projectile after its lifetime expires
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Handle collision (e.g., damage, effects)
        Debug.Log($"Projectile hit {collision.gameObject.name}");
        Destroy(gameObject); // Destroy on impact
    }
}

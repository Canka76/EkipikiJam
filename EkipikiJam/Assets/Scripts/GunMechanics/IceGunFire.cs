using UnityEngine;

public class IceGunFire : MonoBehaviour
{
    [SerializeField] float speed = 20f;            // Speed of the projectile
    [SerializeField] float lifetime = 5f;         // Time before the projectile is destroyed
    private Rigidbody rb;               // Reference to the Rigidbody

    [SerializeField] float damage = 25f;
    [SerializeField] string damageType = "Basic";
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
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(damage, "Ice");
        }
        
        Debug.Log($"Projectile hit {collision.gameObject.name}");
        Destroy(gameObject); // Destroy on impact
    }
}

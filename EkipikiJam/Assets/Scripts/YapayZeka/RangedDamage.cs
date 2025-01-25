using UnityEngine;

public class RangedDamage : MonoBehaviour
{
    public float speed = 20f;            // Speed of the projectile
    public float lifetime = 5f;         // Time before the projectile is destroyed
    private Rigidbody rb;               // Reference to the Rigidbody
    
    [SerializeField] float damage = 25f;
    [SerializeField] string damageType = "Basic";

    public GameObject player;
    HealthManager healthManager;
    
    
    void Start()
    {
        // Ensure Rigidbody is assigned
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Set velocity based on the object's forward direction
            rb.linearVelocity = transform.forward * speed;
        }
        
        healthManager = player.GetComponent<HealthManager>();

        // Destroy the projectile after its lifetime expires
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        healthManager.TakeDamage(damage);
        Debug.Log($"Projectile hit {collision.gameObject.name}");
        Destroy(gameObject); // Destroy on impact
    }
}
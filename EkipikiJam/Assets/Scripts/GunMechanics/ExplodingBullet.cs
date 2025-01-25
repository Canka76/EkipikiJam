using UnityEngine;

public class ExplodingObject : MonoBehaviour
{
    public float growthRate = 5f;         // Speed at which the object grows
    public float maxSize = 3f;           // Maximum size before the explosion ends
    public float explosionDuration = 1f; // How long the object stays after exploding
    public bool destroyAfterExplosion = true; // Whether to destroy the object after exploding

    private bool isExploding = false;    // Tracks whether the object is currently exploding
    private Vector3 initialScale;        // The initial scale of the object
    private float explosionTimer = 0f;   // Timer for the explosion duration
    
    public float speed = 20f;            // Speed of the projectile
    public float lifetime = 5f;         // Time before the projectile is destroyed
    private Rigidbody rb; 

    void Start()
    {
        // Record the initial scale
        initialScale = transform.localScale;
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
        // Trigger explosion on collision
        if (!isExploding)
        {
            Debug.Log($"Collided with {collision.gameObject.name}");
            isExploding = true;
            explosionTimer = 0f; // Reset timer
        }
    }

    void Update()
    {
        if (isExploding)
        {
            HandleExplosion();
        }
    }

    private void HandleExplosion()
    {
        // Gradually increase the size of the object
        if (transform.localScale.magnitude < maxSize)
        {
            transform.localScale += Vector3.one * growthRate * Time.deltaTime;
        }

        // Track how long the explosion lasts
        explosionTimer += Time.deltaTime;
        if (explosionTimer >= explosionDuration)
        {
            if (destroyAfterExplosion)
            {
                Destroy(gameObject);
            }
            else
            {
                isExploding = false;
                transform.localScale = initialScale; // Reset size
            }
        }
    }
}
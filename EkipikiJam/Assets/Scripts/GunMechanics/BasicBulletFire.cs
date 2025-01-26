using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunFire : MonoBehaviour
{
    public float speed = 20f;            // Speed of the projectile
    public float lifetime = 5f;         // Time before the projectile is destroyed
    private Rigidbody rb;               // Reference to the Rigidbody
    
    [SerializeField] float damage = 25f;
    [SerializeField] string damageType = "Basic";

    /*IEnumerator wait()
    {
        yield return new WaitForSeconds(0.1f);
    }*/
    
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
       // StartCoroutine(wait());
        if(collision.gameObject.tag == "Enemy")
            {
                IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.TakeDamage(damage, "Basic");
                }
            }
        Debug.Log($"Projectile hit {collision.gameObject.name}");
        Destroy(gameObject); // Destroy on impact
    }
}

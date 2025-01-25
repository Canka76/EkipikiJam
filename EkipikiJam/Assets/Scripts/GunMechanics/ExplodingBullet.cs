using System;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    public float explosionRadius = 5f;      // Radius of the explosion
    public float explosionDamage = 25f;    // Damage dealt by the explosion
    
    void Explode()
    {

        // Detect all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            // Check if the object has a component that can take damage
            IDamageable damageable = nearbyObject.GetComponent<IDamageable>();

            if (damageable != null)
            {
                // Apply damage to the object
                damageable.TakeDamage(explosionDamage, "Explosion");

                Debug.Log(nearbyObject.name);
            }
        }
        
    }
    

    private void OnCollisionEnter(Collision other)
    {
        Explode();
    }
}
    

// Interface for objects that can take damage
public interface IDamageable
{
    void TakeDamage(float damage, string damageType);
}
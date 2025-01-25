using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject explosionEffect;  // Explosion effect prefab
    public GameObject fireZonePrefab;  // Fire zone prefab (hurting zone)
    public float fireZoneDuration = 5f; // Duration of the fire zone

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Firezone")
        {    // Trigger the explosion effect
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            // Spawn the hurting zone
            if (fireZonePrefab != null)
            {
                GameObject fireZone = Instantiate(fireZonePrefab, collision.contacts[0].point, Quaternion.identity);

                // Destroy the fire zone after the specified duration
                Destroy(fireZone, fireZoneDuration);
            }

            // Destroy the fireball
            Destroy(gameObject);
        }
    }
}

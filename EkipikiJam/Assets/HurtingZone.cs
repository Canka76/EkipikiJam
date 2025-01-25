using UnityEngine;

public class HurtingZone : MonoBehaviour
{
    public float damagePerSecond = 10f; // Damage applied per second
    public string targetTag = "Player"; // Tag of objects that take damage

    void OnTriggerStay(Collider other)
    {
        // Check if the object entering the zone has the correct tag
        if (other.CompareTag(targetTag))
        {
            // Try to find the HealthManager component on the object
            HealthManager healthManager = other.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                // Apply damage over time
                healthManager.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }
}

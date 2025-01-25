using System;
using Unity.VisualScripting;
using UnityEngine;
public class EnemyDamageReceiver : MonoBehaviour, IDamageable
{
    [SerializeField] float health = 100f;
    
    public void TakeDamage(float damage, string damageType)
    {
        Debug.Log("Damaged");
        Debug.Log($"Damage type {damageType}");
        Debug.Log($"Health {health}");
        Debug.Log($"Damage {damage}");
        // Modify damage based on type
        if (damageType == "Explosion")
        {
            damage *= 2f; // Example: Explosion damage is more effective
        }
        else if (damageType == "Ice")
        {
            damage *= 1.3f; // Example: Ice damage is less effective
        }
        else if (damageType == "Basic")
        {
            damage *= 0.9f;
        }
        else if (damageType == "Ultimate")
        {
            damage *= 5f;
        }

        health -= damage;
        Debug.Log($"{gameObject.name} took {damage} {damageType} damage. Remaining health: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} has been destroyed!");
        Destroy(gameObject); // Destroy the object when health reaches zero
    }
}
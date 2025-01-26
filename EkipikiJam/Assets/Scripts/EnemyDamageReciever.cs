using UnityEngine;

public class EnemyDamageReceiver : MonoBehaviour, IDamageable
{
    [SerializeField] float health = 100f;
    [SerializeField] float scalefactor = 1.2f;
    

    public void TakeDamage(float damage, string damageType)
    {
        // Script aktif değilse ya da nesne sahnede aktif değilse hasar almayı engelle
        if (!this.enabled || !gameObject.activeInHierarchy)
        {
            Debug.Log($"{gameObject.name} is inactive or script is disabled. Cannot take damage.");
            return;
        }

        Debug.Log("Damaged");
        Debug.Log($"Damage type: {damageType}");
        Debug.Log($"Health: {health}");
        Debug.Log($"Damage: {damage}");

        // Hasar türüne göre hasar değerini ayarla
        if (damageType == "Explosion")
        {
            damage *= 2f; // Patlama hasarı daha etkili
        }
        else if (damageType == "Ice")
        {
            damage *= 1.3f; // Buz hasarı daha az etkili
        }
        else if (damageType == "Basic")
        {
            damage *= 0.9f;
        }
        else if (damageType == "Ultimate")
        {
            damage *= 5f;
        }

        // Sağlığı azalt
        health -= damage;
        Debug.Log($"{gameObject.name} took {damage} {damageType} damage. Remaining health: {health}");

        // Sağlık sıfır veya altına düştüyse yok et
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} has been destroyed!");
        Destroy(gameObject); // Nesneyi yok et
    }
}

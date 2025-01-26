using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDamageReceiver : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100f;
    [SerializeField] private float scaleFactor = 1.2f; // Scale multiplier for growth
    [SerializeField] private float scaleDuration = 0.2f; // Duration of scale effect

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

        // Start the scale effect
        StartCoroutine(ScaleEffect());

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

    private IEnumerator ScaleEffect()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * scaleFactor;

        // Scale up
        float timer = 0f;
        while (timer < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / scaleDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;

        // Scale down
        timer = 0f;
        while (timer < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, timer / scaleDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
    }
}

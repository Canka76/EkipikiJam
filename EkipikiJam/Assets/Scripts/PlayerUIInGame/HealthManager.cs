using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float maxHealth = 100f;
    [Range(0f, 100f)]
    public float currentHealth = 75f;
    public Image healthBar;
    public Image health;

    void Start()
    {
        health.color = Color.magenta;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        UpdateHealthBar();
    }
    
    public void Heal(float heal)
    {
        currentHealth = Mathf.Clamp(currentHealth + heal, 0, maxHealth);
        UpdateHealthBar();
    }

    
    void UpdateHealthBar()
    {
        health.fillAmount = (currentHealth / maxHealth);
        health.color = Color.Lerp(Color.red, Color.magenta, healthBar.fillAmount);
    }
}

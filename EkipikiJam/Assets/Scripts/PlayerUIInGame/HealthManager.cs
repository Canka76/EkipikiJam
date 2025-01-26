using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float maxHealth = 100f;
    [Range(0f, 100f)]
    public float currentHealth = 75f;
    public Image healthBar;
    public Image health;
    public GameObject gameOverPanel;
    
    private EscapeMenu escapeMenu;

    private void Awake()
    {
        escapeMenu = GetComponent<EscapeMenu>();
    }

    void Start()
    {
        health.color = Color.magenta;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        UpdateHealthBar();
        if (gameOverPanel != null && currentHealth == 0)
        {
            var width = gameOverPanel.GetComponent<RectTransform>().rect.width;
            gameOverPanel.transform.position -= new Vector3(width, 0, 0);
            if (escapeMenu != null)
            {
                escapeMenu.PauseGame(gameOverPanel);
            }
        }
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

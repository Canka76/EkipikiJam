using System;
using UnityEngine;

public class RegenPool : MonoBehaviour
{
    public GameObject player;
    
    public float healthMax = 30f;
    public float healthRegenPerSecond = 1f;
    
    private float healthCount = 0f;
    private HealthManager healthManager;
    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    
    private void Start()
    {
        healthManager = player.GetComponent<HealthManager>();
    }
    

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<FpsController>() != null)
        {
            if (healthCount <= healthMax)
            {
                Debug.Log("Can: " + healthManager.currentHealth);
                healthCount += healthRegenPerSecond * Time.deltaTime;
                healthManager.Heal(healthRegenPerSecond * Time.deltaTime);
            }
        }
    }
}

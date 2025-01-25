using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats", order = 1)]
public class EnemyStats : ScriptableObject
{
    public string enemyName = "Default Enemy"; // Name of the enemy
    public float maxHealth = 100f;             // Maximum health
    public float damage = 10f;                 // Damage dealt by the enemy
    public float speed = 5f;                   // Movement speed

    
}
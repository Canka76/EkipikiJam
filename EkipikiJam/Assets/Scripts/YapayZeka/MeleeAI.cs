using UnityEngine;

public class MeleeAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public float chaseRange = 25f;
    public float attackRange = 2f;

    [Header("Attack Settings")]
    public float attackRate = 1f;
    public float attackDamage = 10f;

    [Header("References")]
    public Transform player;

    private UnityEngine.AI.NavMeshAgent agent;
    private float fireTimer;

    private enum EnemyState { Chasing, Attacking }
    private EnemyState currentState;
    
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        currentState = EnemyState.Chasing;
    }
    
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case EnemyState.Chasing:
                ChasePlayer();
                if (distanceToPlayer <= attackRange)
                {
                    currentState = EnemyState.Attacking;
                }
                break;

            case EnemyState.Attacking:
                AttackPlayer();
                if (distanceToPlayer > attackRange)
                {
                    currentState = EnemyState.Chasing;
                }
                break;
        }
    }
    
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    
    private void AttackPlayer()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= attackRate)
        {
            player.GetComponent<HealthManager>().TakeDamage(attackDamage);
            fireTimer = 0f;
        }
    }
    
    
    
    
    
    
    
}

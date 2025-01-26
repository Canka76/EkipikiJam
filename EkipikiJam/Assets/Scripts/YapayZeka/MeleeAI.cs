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

        // Find player by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player with tag 'Player' not found!");
        }

        currentState = EnemyState.Chasing;
    }

    void Update()
    {
        if (player == null) return; // Exit if player reference is missing

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
        if (agent != null && player != null)
        {
            agent.SetDestination(player.position);
        }
    }

    private void AttackPlayer()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= attackRate)
        {
            if (player != null)
            {
                HealthManager healthManager = player.GetComponent<HealthManager>();
                if (healthManager != null)
                {
                    healthManager.TakeDamage(attackDamage);
                }
                else
                {
                    Debug.LogError("Player does not have a HealthManager component!");
                }
            }
            fireTimer = 0f;
        }
    }
}

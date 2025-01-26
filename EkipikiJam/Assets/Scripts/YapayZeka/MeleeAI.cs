using UnityEngine;
using UnityEngine.AI;

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

    private NavMeshAgent agent;
    private float attackTimer;

    private enum EnemyState { Idle, Chasing, Attacking }
    private EnemyState currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Find player by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player with tag 'Player' not found!");
            enabled = false; // Disable script to avoid errors
            return;
        }

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component not found on the enemy!");
            enabled = false; // Disable script if NavMeshAgent is missing
            return;
        }

        currentState = EnemyState.Idle;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayerSqr = (player.position - transform.position).sqrMagnitude;
        float chaseRangeSqr = chaseRange * chaseRange;
        float attackRangeSqr = attackRange * attackRange;

        switch (currentState)
        {
            case EnemyState.Idle:
                if (distanceToPlayerSqr <= chaseRangeSqr)
                {
                    currentState = EnemyState.Chasing;
                }
                break;

            case EnemyState.Chasing:
                if (distanceToPlayerSqr > chaseRangeSqr)
                {
                    currentState = EnemyState.Idle;
                    agent.SetDestination(transform.position); // Stop movement
                }
                else if (distanceToPlayerSqr <= attackRangeSqr)
                {
                    currentState = EnemyState.Attacking;
                }
                else
                {
                    ChasePlayer();
                }
                break;

            case EnemyState.Attacking:
                if (distanceToPlayerSqr > attackRangeSqr)
                {
                    currentState = EnemyState.Chasing;
                }
                else
                {
                    AttackPlayer();
                }
                break;
        }
    }

    private void ChasePlayer()
    {
        if (agent != null && player != null)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);

            // Smoothly rotate towards the player
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    private void AttackPlayer()
    {
        agent.isStopped = true; // Stop moving while attacking

        // Smoothly look at the player while attacking
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        // Handle attack timing
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackRate)
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
            attackTimer = 0f; // Reset the timer after attacking
        }
    }
}

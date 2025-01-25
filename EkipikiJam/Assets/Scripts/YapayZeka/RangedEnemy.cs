using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : MonoBehaviour
{
    [Header("Movement Settings")]
    public float chaseRange = 25f;
    public float attackRange = 10f;

    [Header("Attack Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1f;

    [Header("References")]
    public Transform player;

    private NavMeshAgent agent;
    private float fireTimer;

    private enum EnemyState { Chasing, Attacking }
    private EnemyState currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Find player and projectile by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player with tag 'Player' not found!");
        }

        projectilePrefab = GameObject.FindGameObjectWithTag("EnemyProjectile");
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile with tag 'EnemyProjectile' not found!");
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
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position); // Stop moving
        transform.LookAt(player);

        fireTimer += Time.deltaTime;
        if (fireTimer >= 1f / fireRate)
        {
            FireProjectile();
            fireTimer = 0f;
        }
    }

    private void FireProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.None;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

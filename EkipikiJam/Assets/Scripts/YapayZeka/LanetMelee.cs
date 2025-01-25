using UnityEngine;
using UnityEngine.AI;

public class MeleeCharacterAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform target; // Target to follow
    public float stoppingDistance = 2f; // Distance to stop and attack

    [Header("Attack Settings")]
    public float attackCooldown = 1.5f; // Time between attacks
    public int attackDamage = 10; // Damage dealt per attack
    public float attackRotationAngle = 45f; // Angle to rotate towards target before attacking

    private NavMeshAgent agent;
    private Animator animator; // For controlling animations
    private float attackTimer;

    void Start()
    {
        // Initialize components
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (target == null)
        {
            Debug.LogError("Target not set for MeleeCharacterAI.");
        }
    }

    void Update()
    {
        if (target == null)
            return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // Move towards the target
        if (distanceToTarget > stoppingDistance)
        {
            agent.SetDestination(target.position);
            agent.isStopped = false;
            animator.SetBool("isMoving", true);
        }
        else
        {
            agent.isStopped = true;
            animator.SetBool("isMoving", false);

            // Rotate towards target and attack logic
            RotateTowardsTarget();

            if (attackTimer <= 0)
            {
                Attack();
                attackTimer = attackCooldown;
            }
        }

        // Cooldown timer
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    void RotateTowardsTarget()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        Quaternion targetRotation = Quaternion.RotateTowards(transform.rotation, lookRotation, attackRotationAngle);
        transform.rotation = targetRotation;
    }

    void Attack()
    {
        // Trigger attack animation
        animator.SetTrigger("Attack");

        // Check if the target has a health component (optional)
       
        
    }
}

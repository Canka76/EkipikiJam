using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AdjustableObject : MonoBehaviour, IAdjustableSpeed
{
    private int hitCount = 0;  // Number of hits
    private NavMeshAgent navMeshAgent; // Reference to NavMeshAgent
    private float baseSpeed = 10f; // Default base speed
    private bool isStunned = false; // Check if currently stunned

    [SerializeField] private float stunDuration = 2f; // Duration of the stun

    private void Awake()
    {
        // Ensure the NavMeshAgent component is retrieved
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component is required but not found!");
        }
        else
        {
            navMeshAgent.speed = baseSpeed; // Initialize with base speed
        }
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
    }

    public void AdjustSpeed()
    {
        if (isStunned)
        {
            Debug.Log("Object is stunned. No adjustments allowed until stun ends.");
            return;
        }

        hitCount++;

        if (hitCount == 1)
        {
            // Halve the NavMeshAgent speed
            navMeshAgent.speed /= 2f;
            Debug.Log($"NavMeshAgent speed halved to {navMeshAgent.speed}");
        }
        else if (hitCount == 2)
        {
            // Start the stun and stop movement
            StartCoroutine(StunCoroutine());
        }
        else
        {
            Debug.Log("No further adjustments needed.");
        }
    }

    private IEnumerator StunCoroutine()
    {
        isStunned = true;

        // Set speed to zero to simulate stun
        navMeshAgent.speed = 0f;
        Debug.Log($"NavMeshAgent stunned for {stunDuration} seconds.");

        // Wait for the stun duration
        yield return new WaitForSeconds(stunDuration);

        // Resume with base speed and reset hitCount to allow further adjustments
        navMeshAgent.speed = baseSpeed;
        isStunned = false;
        hitCount = 0;  // Reset hitCount to allow reuse

        Debug.Log($"Stun ended. NavMeshAgent speed restored to {navMeshAgent.speed}");
    }
}

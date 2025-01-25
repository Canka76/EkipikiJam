using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    private Transform playerTransform;
    public Transform agentTransform;
    NavMeshAgent agent;
    public float distanceFollow = 10f;
    public float rotationSpeed = 2f;
    private bool isOrbiting = false; 

    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        agent.updateRotation = false;
    }

    void Update()
    {
        float distance = Vector3.Distance(agentTransform.position, playerTransform.position);

        if (distance > distanceFollow + 0.5f)
        {
            MoveToTarget(distanceFollow);
        }
        else if (distance < distanceFollow - 0.5f)
        {
            MoveToTarget(-distanceFollow);
        }
        else
        {
            agent.SetDestination(agentTransform.position);
        }

        LookAtPlayer();
    }

    void MoveToTarget(float offsetDistance)
    {
        Vector3 direction = (agentTransform.position - playerTransform.position).normalized;
        Vector3 targetPosition = playerTransform.position - direction * offsetDistance;
        agent.SetDestination(targetPosition);
    }

    void LookAtPlayer()
    {
        Vector3 direction = playerTransform.position - transform.position;
        direction.y = 0;
        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }
}

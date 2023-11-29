using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardShootPlayer : MonoBehaviour
{
    public Transform target; // leave the target empty in the Inspector
    public Transform player;

    private UnityEngine.AI.NavMeshAgent navMeshAgent;

    public float normalSpeed = 0f;
    public float shootingDistance;

    public GuardManager guard;
    public GuardMind guardMind;


    void Start()
    {
        guard.shootAnim();

        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found on this object.");
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > shootingDistance)
        {
            StopAllCoroutines();
            guard.FollowPlayer();
        }


        if (navMeshAgent != null && target != null)
        {
            // Set the destination of the NavMeshAgent to the target's position
            navMeshAgent.SetDestination(target.position);

            // Rotate the object to look at the target
            RotateTowardsTarget();

                navMeshAgent.speed = normalSpeed;
                target = player;
            
        }
    }

    void RotateTowardsTarget()
    {
        // Get the direction to the target
        Vector3 directionToTarget = target.position - transform.position;

        // Ignore the y-component to avoid tilting
        directionToTarget.y = 0;

        // Rotate towards the target
        if (directionToTarget != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardFollowPlayer : MonoBehaviour
{
    public Transform target; // leave the target empty in the Inspector
    public Transform wayPoint;
    public Transform player;

    private NavMeshAgent navMeshAgent;
    private bool isLowSpeed = false;

    public float normalSpeed = 3.5f;
    public float lowSpeed = 1.5f;
    public float shootingDistance;

    public GuardManager guard;
    public GuardMind guardMind;

    bool isShooting;

    void Start()
    {
        isLowSpeed = false;

        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found on this object.");
        }
    }
    void OnEnable()
    {
        if (!guard.anim.GetBool("isRunning"))
        {
            guard.shootAnim();
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < shootingDistance && !isShooting)
        {
            guard.ShootPlayer();
        }



        if (navMeshAgent != null && target != null)
        {
            // Set the destination of the NavMeshAgent to the target's position
            navMeshAgent.SetDestination(target.position);

            // Rotate the object to look at the target
            RotateTowardsTarget();

            // Check if low speed state is active
            if (isLowSpeed)
            {
                navMeshAgent.speed = lowSpeed;
                target = wayPoint;
            }
            else
            {
                navMeshAgent.speed = normalSpeed;
                target = player;
            }
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

    // Switch to low speed state
    public void SetLowSpeedState(bool state)
    {
        isLowSpeed = state;
    }
}

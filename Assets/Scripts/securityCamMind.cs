using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class securityCamMind : MonoBehaviour
{
    public static event System.Action OnGuardManSpottedPlayer;

    public float waitTime = .3f;
    public float turnSpeed = 90;
    public float timeToSpotPlayer = 0.5f;

    public Light spotLight;
    public float viewDistance;
    public LayerMask viewMask;

    float viewAngle;
    float playerVisibleTimer;

    Transform player;
    Color originalSpotlightColour;

    public Animator anim;

    public bool playerSpotted;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotLight.spotAngle;
        originalSpotlightColour = spotLight.color;
    }

    void Update()
    {
        if (CanSeePlayer()) // player spotted
        {
            playerVisibleTimer += Time.deltaTime;

        }
        else //player not spotted
        {
            playerVisibleTimer -= Time.deltaTime;
        }
        playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSpotPlayer);
        spotLight.color = Color.Lerp(originalSpotlightColour, Color.red, playerVisibleTimer / timeToSpotPlayer);

        if (playerVisibleTimer >= timeToSpotPlayer)
        {
            if (OnGuardManSpottedPlayer != null)
                OnGuardManSpottedPlayer();

            RotateTowardsTarget();

            anim.enabled = false;

            playerSpotted = true;

            StopAllCoroutines();
        }

    }

    public bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, player.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    void RotateTowardsTarget()
    {
        // Get the direction to the target
        Vector3 directionToTarget = player.position - transform.position;

        // Ignore the y-component to avoid tilting
        //directionToTarget.y = 0;

        // Rotate towards the target
        if (directionToTarget != Vector3.zero && CanSeePlayer())
        {
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
}


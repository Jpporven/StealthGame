using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardManager : MonoBehaviour
{
    public GuardMind guardPatrol;
    public GuardFollowPlayer guardFollow;
    public GuardShootPlayer guardShoot;

    public GameObject muzzleFlash;
    public Animator anim;
    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    public Vector3 currentVelocity;
    public float velocity;

    private Vector3 previousPosition;
    private float lastUpdateTime;

    bool isShooting = false;

    void Start()
    {
        previousPosition = transform.position;
        lastUpdateTime = Time.time;
    }

    void LateUpdate()
    {
        float deltaTime = Time.time - lastUpdateTime;

        currentVelocity = (transform.position - previousPosition) / deltaTime;

        velocity = currentVelocity.magnitude;

        anim.SetFloat("Velocity", velocity);

        previousPosition = transform.position;
        lastUpdateTime = Time.time;

        if(velocity <= 0.1f && isShooting)
        {
            muzzleFlash.SetActive(true);
        }
        else
        {
            muzzleFlash.SetActive(false);
        }
    }

    public void FollowPlayer()
    {
        guardPatrol.enabled = false;
        guardFollow.enabled = true;
        guardShoot.enabled = false;

        isShooting = false; 
    }
    public void ReturnToPatrol()
    {
        guardPatrol.enabled = true;
        guardFollow.enabled = false;
        guardShoot.enabled = false;

        isShooting = false;
    }
    public void ShootPlayer()
    {
        guardPatrol.enabled = false;
        guardFollow.enabled = false;
        guardShoot.enabled = true;

        isShooting = true;
    }

    public void runAnim()
    {
        anim.SetBool("isShooting", false);
        anim.SetBool("isRunning", true);
        anim.SetBool("isDead", false);
        StartCoroutine(animReset("isRunning"));
    }
    public void shootAnim()
    {
        anim.SetBool("isShooting", true);
        anim.SetBool("isRunning", false);
        anim.SetBool("isDead", false);
        StartCoroutine(animReset("isShooting"));
    }
    public void deathAnim()
    {
        anim.SetBool("isShooting", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isDead", true);
        StartCoroutine(animReset("isDead"));
    }
    public void hitAnim()
    {
        anim.SetBool("isHit", true);
        StartCoroutine(hitReset());
    }

    IEnumerator animReset(string animation)
    {
        yield return new WaitForSeconds(0.5f);

        anim.SetBool(animation, false);
    }

    IEnumerator hitReset()
    {
        yield return new WaitForSeconds(3f);

        anim.SetBool("isHit", false);
    }
}

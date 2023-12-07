using UnityEngine;
using UnityEditor.Animations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    //referencing the animator
    [HideInInspector]
    public Animator anim;
    //player speed
    public float speed = 3f;
    //Camera gameobject
    public Transform cam;
    //player charater controller component
    CharacterController Controller;
    //variable to smooth out the turn speed, instead of making it snappy;
    public float turnSmmothTime = 0.1f;
    float turnSmoothVelocity;
    //variables for finding the player's velocity
    public float playerVelocity;


    void Start()
    {
        anim = this.GetComponent<Animator>();
        Controller = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        anim.SetFloat("Velocity", playerVelocity);

        //Set the velocity in the animator
        anim.SetFloat("Velocity", playerVelocity);

        // Get input from the keyboard
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //Get the velocity
        playerVelocity = new Vector3(horizontal, 0, vertical).magnitude;

        //triggering the walking animation with bool
        bool walking = horizontal != 0f || vertical != 0f;
        anim.SetBool("isWalking", walking);
        running();

        //Store our direction
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //Check if we are moving in any direction
        if (direction.magnitude >= 0.1f)
        {
            //Determine camera rotation based on player direction, and have the camera direction become our foward direction
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //how to smooth the turn speed
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmmothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            //have the direction we're looking at, become the foward direction
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Controller.Move(moveDir * speed * Time.deltaTime);
        }

    }
    void running()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("isRunning", true);
            speed *= 2f;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("isRunning", false);
            speed /= 2f;
        }

    }

    public void AnimSwap(GameObject victim, Animator v_anim)
    {
        this.GetComponent<Animator>().enabled = false;
        anim = v_anim;
    }
}

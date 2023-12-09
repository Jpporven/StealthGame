using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggers : MonoBehaviour
{
    //the positions the door will move to when the door moves up/down
    public Transform doorDownPos;
    public Transform doorUpPos;
    //the speed at which the door moves
    public float doorSpeed;
    //conditions for the door to be opened
    public bool allowsPlayer;
    public bool allowsScientist;
    public bool allowsGuard;

    bool playerInRange;


    // Set the starting position of the door to be up
    void Start()
    {
        transform.position = doorUpPos.position;
    }

    private void OnTriggerEnter(Collider other)
    {
            /* if the player is in range of the door, and allows the tag the player has when entering the trigger collider
             * of the door, disable the collider and move the door down.
             */

            if (other.gameObject.tag == "Scientist" && allowsScientist)
            {
                playerInRange = true;
                this.GetComponent<Collider>().enabled = false;

            }else if (other.gameObject.tag == "Guard" && allowsGuard)
            {
                playerInRange = true;
                this.GetComponent<Collider>().enabled = false;

            }
            else if (other.gameObject.tag == "Player" && allowsPlayer)
            {
                playerInRange = true;
                this.GetComponent<Collider>().enabled = false;

            }


    }

    /* when the player leaves the trigger collider of the door, reenable the collider and set the position of the 
     * door to be back up.
    */
    private void OnTriggerExit(Collider other)
    {
        this.GetComponent<Collider>().enabled = true;
        playerInRange = false;
    }

    private void FixedUpdate()
    {
        if(playerInRange)
        {
            transform.position = Vector3.Lerp(doorDownPos.position, transform.position, Time.deltaTime / doorSpeed);
        }
        else
        {
            transform.position = Vector3.Lerp(doorUpPos.position, transform.position, Time.deltaTime / doorSpeed);
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skinSnatch : MonoBehaviour
{
    GameObject victim;
    GameObject[] victimsBody;
    GameObject[] playersBody;
    public PlayerMovement playerMove;

    bool isDisgused = false;
    bool inVictimRange = false;
    void Start()
    {
        playersBody = new GameObject[2];
        victimsBody = new GameObject[2];
        for (int i = 0; i < transform.childCount; i++)
        {
            playersBody[i] = transform.GetChild(i).gameObject;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isDisgused) skinSteal();
            else if (isDisgused && !inVictimRange)
            {
                Destroy(victim);
                revertSkin();
                //playerMove.AnimSwap(this.gameObject);
                isDisgused = false;
            }
            else print("Cant Change in Others skin");     
        }  
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!isDisgused)
        {
            victim = other.transform.parent.gameObject;
            if (other.name == "AmbushCollider")
            {
                getVictimsBody(victim);
                inVictimRange = true;
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "AmbushCollider")
        {
            inVictimRange=false;
            victimsBody = new GameObject[2];
        }
    }
    void getVictimsBody(GameObject victim)
    {
        for (int i = 0; i < 2; i++)
        {
            victimsBody[i] = victim.transform.GetChild(i).gameObject;
        }
    }
    void revertSkin()
    {
        for (int i = 0; i < playersBody.Length; i++)
        {
            playersBody[i].SetActive(true);
        }
    }
    void skinSteal()
    {
        if(victim != null)
        {
            Destroy(victim.transform.GetChild(2).gameObject);
            for (int i = 0; i < victimsBody.Length; i++)
            {
                //Aligns Victim
                victim.transform.position = new Vector3(transform.position.x, victim.transform.position.y, transform.position.z);
                victim.transform.rotation = transform.rotation;
            }
            for (int i = 0; i < playersBody.Length; i++)
            {
                playersBody[i].SetActive(false);
            }
            victim.transform.parent = transform;
            //Anim swap
            playerMove.AnimSwap(victim);
            isDisgused = true;
            inVictimRange = false;
            victimsBody = new GameObject[2];
        }
        else
        {
            print("Victim Not in Range");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class skinSnatch : MonoBehaviour
{
    GameObject victim;
    public GameObject[] possibleVictims;
    Animator v_Animator;
    GameObject[] playersBody;

    PlayerMovement playerMove;
    int confirmedSkin;

    bool isDisgused = false;
    bool inVictimRange = false;
    bool hasSkin;

    void Start()
    {

        startSkins();
        playerMove = GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isDisgused) skinSteal();
            else if (isDisgused && !inVictimRange)
            {
                Destroy(victim);
                revertSkin();
                this.gameObject.tag = "Player";
                isDisgused = false;
            } 
            
        }  
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!isDisgused)
        {
            victim = other.transform.parent.gameObject;
            checkSkins();
            if (other.name == "AmbushCollider" && hasSkin)
            {
                inVictimRange = true;
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "AmbushCollider")
        {
            inVictimRange=false;
        }
    }

    void startSkins()
    {
        playersBody = new GameObject[2];
        for (int i = 0; i < 2; i++)
        {
            playersBody[i] = transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < possibleVictims.Length; i++)
        {
            possibleVictims[i].SetActive(false);
            
        }
    }

    void revertSkin()
    {
        for (int i = 0; i < 2; i++)
        {
            playersBody[i].SetActive(true);
        }
    }
    void skinSteal()
    {
        if (victim != null)
        {
            getVictimSkin();
            for (int i = 0; i < 2; i++)
            {
                playersBody[i].SetActive(false);
            }
            //Anim swap
            playerMove.anim = v_Animator;

            playerMove.AnimSwap(victim);

            isDisgused = true;
            inVictimRange = false;
        }
    }
    void checkSkins()
    {
        for (int i = 0; i < possibleVictims.Length; i++)
        {
            if (victim.gameObject.tag == possibleVictims[i].gameObject.tag)
            {
                hasSkin = true;
                confirmedSkin = i;
                v_Animator = possibleVictims[confirmedSkin].GetComponent<Animator>();
                return;
            }
        }
        hasSkin = false;
    }

    void getVictimSkin()
    {
        Destroy(victim);
        victim = possibleVictims[confirmedSkin];
        possibleVictims[confirmedSkin].SetActive(true);
        this.gameObject.tag = victim.gameObject.tag;
        return;
    }
}

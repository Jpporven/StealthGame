using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class skinSnatch : MonoBehaviour
{
    GameObject victim;
    GameObject v_Collider;
    public GameObject[] possibleVictims;
    public disableVictim disabler;
    Animator v_Animator;
    GameObject[] playersBody;
    PlayerMovement playerMove;
    public playerAnimManager p_AnimManager;
    
    int confirmedSkin;

    bool isDisgused = false;
    bool inVictimRange = false;
    bool hasSkin;
    bool victimDied = false;

    void Start()
    {

        startSkins();
        playerMove = GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //he attacks
            if (!victimDied && !isDisgused && inVictimRange && !playerAnimManager.isReverting && !playerAnimManager.isTransforming && !playerMove.runing && !playerMove.walking) skinSteal();
            else if (isDisgused && !inVictimRange && !playerAnimManager.isAttacking && !playerAnimManager.isTransforming && !playerMove.runing && !playerMove.walking)
            {
                this.gameObject.tag = "Player";
                isDisgused = false;
                playerMove.anim = GetComponent<Animator>();
                p_AnimManager.skinRevertAnim(v_Animator, playerMove.anim, victim, playersBody);                
            }

        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(victimDied && !playerAnimManager.isReverting && !playerAnimManager.isAttacking && !playerMove.runing && !playerMove.walking)
            {
                takeSkin();
            }
            else
            {
                print("All Needs to be true");
                print(victimDied);
                print(!playerAnimManager.isReverting);
                print(!playerAnimManager.isAttacking);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!isDisgused)
        {
            victim = other.transform.parent.gameObject;
            v_Collider = other.gameObject.transform.GetChild(0).gameObject;
            checkSkins();
            if (other.name == "AmbushCollider" && hasSkin)
            {

                if(victim.GetComponent<Animator>().GetBool("isDead"))
                {
                    victimDied = true;
                }
                else inVictimRange = true;
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "AmbushCollider")
        {
            inVictimRange=false;
            StartCoroutine(delayCheck());

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

    void skinSteal()
    {
        if (victim != null)
        {

            Animator victimAnim = victim.GetComponent<Animator>();
            disabler.cripple(confirmedSkin, victim);
            p_AnimManager.skinStealAnim(victim, victimAnim, playerMove.anim, v_Collider);

            inVictimRange = false;
            victimDied = true;
        }
    }
    void takeSkin()
    {
        v_Animator.runtimeAnimatorController = playerMove.anim.runtimeAnimatorController;
        p_AnimManager.skinSwapAnim(playersBody, victim, possibleVictims[confirmedSkin], v_Animator);
        victim = possibleVictims[confirmedSkin];
        isDisgused = true;
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
    IEnumerator delayCheck()
    {
        yield return new WaitForSeconds(2);
        if (!playerAnimManager.isAttacking && !inVictimRange) victimDied = false;
    }

}

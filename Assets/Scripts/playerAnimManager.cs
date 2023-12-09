using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimManager : MonoBehaviour
{
    
    public PlayerMovement playerMovement;
    public Transform player_transform;
    Transform player_Target;
    [HideInInspector]
    public static bool isAttacking;
    [HideInInspector]
    public static bool isTransforming;
    [HideInInspector]
    public static bool isReverting;
    
    Animator player_Animator;
    Animator victim_Animator;
    Animator disguiseAnimator;
    GameObject victimBody;
    GameObject disguise_body;
    
    
    //==========Skin Revert===========
    public void skinRevertAnim(Animator disguise_Anim, Animator player, GameObject disguisebody, GameObject[] p_body)
    {

        if (!isTransforming)
        {
            playerMovement.enabled = false;
            disguiseAnimator = disguise_Anim;
            player_Animator = player;
            disguise_body = disguisebody;
            isReverting = true;
            StartCoroutine(WAS_SkinRevert(p_body));

        }
    }
    void revertSkin(GameObject[] playersBody)
    {
        for (int i = 0; i < 2; i++)
        {
            playersBody[i].SetActive(true);
        }
    }
    //WAS stands for Wait A Sec
    IEnumerator WAS_SkinRevert(GameObject[] player_Body)
    {
        disguiseAnimator.SetTrigger("Revert");
        player_Animator.SetTrigger("Revert");
        yield return new WaitForSeconds(1);
        disguise_body.SetActive(false);
        revertSkin(player_Body);
        disguiseAnimator.runtimeAnimatorController = null;
        StartCoroutine(resumeMovement());
    }
    IEnumerator resumeMovement()
    {
        yield return new WaitForSeconds(1);
        while (!player_Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            yield return null;
        }
        playerMovement.enabled = true;
        isReverting = false;
    }
    //==========Take Down============
    public void skinStealAnim(GameObject v_body, Animator v_anim, Animator p_anim, GameObject v_AnimPos)
    {
        if (!isReverting)
        {
            victimBody = v_body;
            victim_Animator = v_anim;
            player_Animator = p_anim;
            isAttacking = true;
            StartCoroutine(ensureIdle());
            player_transform.rotation = victimBody.transform.rotation;
            player_transform.position = new Vector3(v_AnimPos.transform.position.x, player_transform.position.y, v_AnimPos.transform.position.z);
            player_Target = victimBody.transform;
            StartCoroutine(WAS_Attack());
        }
    }
    IEnumerator ensureIdle()
    {
        while (playerMovement.walking)
        {
            yield return null;
        }
        playerMovement.enabled = false;
        player_Animator.SetTrigger("Attack");
    }
    IEnumerator WAS_Attack()
    {
        yield return new WaitForSeconds(1);
        victim_Animator.SetBool("isDead", true);
        while (!player_Animator.IsInTransition(0))
        {
            yield return null;
        }
        player_transform.position = player_Target.position;
        yield return new WaitForSeconds(1);
        playerMovement.enabled = true;
        isAttacking = false;
    }
    //==========Skin Steal============
    public void skinSwapAnim(GameObject[] player_Body, GameObject v_body, GameObject p_newSkin, Animator p_newAnim)
    {
        isTransforming = true;
        playerMovement.enabled = false;
        victimBody = v_body;
        disguise_body = p_newSkin;
        disguiseAnimator = p_newAnim;
        player_Animator.SetTrigger("Revert");


        StartCoroutine(delayResume(player_Body));
    }
    IEnumerator delayResume(GameObject[] player_Body)
    {
        for (int i = 0; i < 2; i++)
        {
            player_Body[i].SetActive(false);
        }
        Destroy(victimBody);
        victimBody = disguise_body;
        victimBody.SetActive(true);

        disguise_body.GetComponent<Animator>().SetTrigger("Revert");
        this.gameObject.tag = victimBody.gameObject.tag;
        playerMovement.anim = disguiseAnimator;
        yield return new WaitForSeconds(3);
        playerMovement.enabled = true;
        isTransforming = false;
    }
}

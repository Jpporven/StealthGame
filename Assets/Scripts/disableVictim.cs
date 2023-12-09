using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class disableVictim : MonoBehaviour
{
    public void cripple(int victimType, GameObject victimGameObject)
    {
        if(victimType == 0)
        {
            disableGuardMovement(victimGameObject);
        }
    }

    void disableGuardMovement(GameObject guard)
    {
        guard.GetComponent<GuardMind>().speed = 0;
        guard.GetComponent<GuardMind>().turnSpeed = 0;
        guard.GetComponent<GuardManager>().enabled = false;
        guard.GetComponent <GuardMind>().enabled = false;
        guard.GetComponent<Collider>().enabled = false;
    }
}


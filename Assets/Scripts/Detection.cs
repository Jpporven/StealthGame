using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public void OnTriggerEnter3D (Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.tag = "Default";
        }
    }
    public void OnTriggerExit3D (Collision other)
    {
        if (other.gameObject.tag == "Default")
        {
            gameObject.tag = "Player";
        }
    }
    
}

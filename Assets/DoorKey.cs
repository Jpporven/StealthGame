using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : MonoBehaviour
{
    public GameObject textPrompt;
    public Transform playerCam;
    public DoorTriggers door;
    public bool Lvl2Key;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Guard" || other.gameObject.tag == "Player")
        {
            if(Lvl2Key)
            {
                Destroy(this.gameObject);
                door.allowsGuard = true;
                door.allowsPlayer = true;
            }
            door.allowsPlayer = true;
            textPrompt.SetActive(true);
        }

    }

}

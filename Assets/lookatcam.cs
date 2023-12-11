using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatcam : MonoBehaviour
{

        public Transform Player;

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(new Vector3(Player.position.x, transform.position.y, Player.position.z));

        }
    
}

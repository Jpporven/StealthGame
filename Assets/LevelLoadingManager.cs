using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoadingManager : MonoBehaviour
{
    /* Everything commented down below is to be uncommented once Van 
     * has made level 3.
     */
    public GameObject level1;
    public GameObject level2;
    //public GameObject level3;
    public GameObject player;

    public float level1DistanceThreshold;
    public float level2DistanceThreshold;
    public float level3DistanceThreshold;

    

    public float distanceFromPlayer;

    void Start()
    {
        level2.SetActive(false);
        //level3.SetActive(false);
    }

    void Update()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);

        if(player.tag == "Scientist")
        {
            level2.SetActive(true);
        }
        //if(player.tag == "Guard")
        //{
        //   level3.SetActive(true);
        //}else
        //{
        //   level3.SetActive(false);
        //}

        if(distanceFromPlayer > level1DistanceThreshold && player.tag != "Scientist")
        {
            level1.SetActive(false);
        }else if (distanceFromPlayer > level2DistanceThreshold && player.tag != "Guard")
        {
            level2.SetActive(false);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoseCondition : MonoBehaviour
{
    public GameObject loseScreen;

    public GameObject fadeOut;

    public GameObject menuFade;

    public GameObject cinemachineCam;

    public GameObject player;

    public GuardMind[] Guard;

    public securityCamMind camera;

    // Start is called before the first frame update
    void Start()
    {
        loseScreen.SetActive(false);
        fadeOut.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
       if(GuardCaughtPlayer())
       {
            StartCoroutine(ActivateLoseScreen());
       }

       if(camera.playerSpotted)
       {
            StartCoroutine(ActivateLoseScreen());
       }
    }


    public bool GuardCaughtPlayer()
    {
        for (int i = 0; i < Guard.Length; i++)
        {
            if (Guard[i].CanSeePlayer() && player.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }  


    public void Restart()
    {
        StartCoroutine(FadeRestart());
    }

    public void MainMenu()
    {
        StartCoroutine(FadeMenu());
    }

    IEnumerator ActivateLoseScreen()
    {
        menuFade.SetActive(true);

        player.GetComponent<PlayerMovement>().enabled = false;

        yield return new WaitForSeconds(2f);

        cinemachineCam.SetActive(false);

        loseScreen.SetActive(true);
    }

    IEnumerator FadeMenu()
    {
        fadeOut.SetActive(true);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator FadeRestart()
    {
        fadeOut.SetActive(true);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("Lvl1");
    }
}

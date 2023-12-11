using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public GameObject cinemachineManager;
    public GameObject fadeOut;
    public PlayerMovement player;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Guard" || other.gameObject.tag == "Player")
        {
            fadeOut.SetActive(true);
            cinemachineManager.SetActive(false);
            player.enabled = false;
            StartCoroutine(LoadCredits());
        }

    }

    IEnumerator LoadCredits()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("MainMenu");
    }
}

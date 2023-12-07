using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject Menu;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject clickPrompt;
    public GameObject fadeScreen;
    public GameObject fadeOutScreen;

    bool delayIsDone = false;
    bool menuActive = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MenuDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if(delayIsDone)
        {
            StopAllCoroutines();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(delayIsDone && !menuActive)
            {
                clickPrompt.SetActive(false);
                fadeScreen.SetActive(true);

                delayIsDone = false;
                StartCoroutine(MainMenuDelay());

            }
        }
    }

    public void StartButton()
    {
        delayIsDone = false;
        StartCoroutine(StartDelay());
    }

    public void BackButton()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void SettingsButton()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    IEnumerator StartDelay()
    {
        fadeOutScreen.SetActive(true);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("CutScene");
    }

    IEnumerator MainMenuDelay()
    {
        menuActive = true;

        yield return new WaitForSeconds(1.5f);

        Menu.SetActive(true);

        delayIsDone = true;
    }

    IEnumerator MenuDelay()
    {
        yield return new WaitForSeconds(1.5f);

        delayIsDone = true;
    }
}

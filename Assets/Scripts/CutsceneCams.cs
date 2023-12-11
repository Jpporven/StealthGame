using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CutsceneCams : MonoBehaviour
{
    public GameObject fadeOut;
    public GameObject surgeon1;
    public GameObject surgeon2;
    public GameObject glassBreakFX;
    public GameObject glass;

    public GameObject[] cameras;
    public GameObject[] lightNoAnim;
    public GameObject[] lightAnim;
    public float[] camDuration;

    // Start is called before the first frame update
    void Start()
    {
        switchCam(0);
    }

    private void switchCam(int i)
    {
       StopAllCoroutines();

       cameras[0].SetActive(false);

       cameras[1].SetActive(false);

       cameras[2].SetActive(false);

       cameras[3].SetActive(false);

       cameras[4].SetActive(false);


        if (i == 4)
        {
            Invoke("sceneFade", 2f);
        }

        cameras[i].SetActive(true);

       


       StartCoroutine(ActivateCam(i));
    }

    void sceneFade()
    {
        fadeOut.SetActive(true);
        Invoke("scenechange", 1f);
    }

    void scenechange()
    {
        SceneManager.LoadScene("Lvl12");
    }

    IEnumerator ActivateCam(int a)
    {
        yield return new WaitForSeconds(camDuration[a]);

        if (a == 3)
        {
            glassBreakFX.SetActive(true);
            glass.SetActive(false);
        }

        if (a == 1)
        {
            lightNoAnim[0].SetActive(false);
            lightNoAnim[1].SetActive(false);
            lightNoAnim[2].SetActive(false);
            lightNoAnim[3].SetActive(false);

            lightAnim[0].SetActive(true);
            lightAnim[1].SetActive(true);
            lightAnim[2].SetActive(true);
            lightAnim[3].SetActive(true);
        }

        if (a == 0)
        {
            surgeon1.SetActive(false);
            surgeon2.SetActive(true);
        }


        a += 1;

        switchCam(a);
    }
}

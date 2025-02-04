using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingMenue : MonoBehaviour
{
    public GameObject transition;
    public Animator animator;


    private void Awake()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }

    public void LoadMenue()
    {

        transition.SetActive(true);
        animator.SetTrigger("TopToBotB");
        StartCoroutine(Load());
        // StartCoroutine(ActivateMenue());

        /*
        Resume();
        SceneManager.LoadScene("MainMenue");
        */
    }

    public void QuitGame()
    {
        transition.SetActive(true);
        animator.SetTrigger("TopToBotB");
        StartCoroutine(ShutDown());
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("MainMenue");
    }

    IEnumerator ShutDown()
    {
        yield return new WaitForSeconds(0.4f);
        Application.Quit();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenue : MonoBehaviour
{
    

    public static bool GamePaused = false;

    [Header("Menues")]
    public GameObject pauseMenueUI;
    public GameObject optionsMenueUI;
    public GameObject gameOverMenueUI;

    [Header("FadeOut/In")]
    public GameObject transition;
    public Animator animator;

    [Header("Player")]
    public GameObject player;
    private Health playerHealthScript;

    public Slider slider;


    private void Awake()
    {
        transition.SetActive(true);
        animator.SetTrigger("TopToBotW");
        StartCoroutine(ActivateScene());
        playerHealthScript = player.GetComponent<Health>();


        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        slider.value = PlayerPrefs.GetFloat("volume");
    }

    void Update()
    {

        if (playerHealthScript.dead)
        {
            StartCoroutine(GameOver());
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GamePaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }



        
    }

    public void Resume()
    {
        pauseMenueUI.SetActive(false);
        optionsMenueUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    void Pause()
    {
        pauseMenueUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void LoadMenue()
    {
        
        transition.SetActive(true);
        animator.SetTrigger("TopToBotMain");
       // StartCoroutine(ActivateMenue());

        /*
        Resume();
        SceneManager.LoadScene("MainMenue");
        */
    }

    public void LoadSettings()
    {
        pauseMenueUI.SetActive(false);
        optionsMenueUI.SetActive(true);
    }

    public void BackButton()
    {
        pauseMenueUI.SetActive(true);
        optionsMenueUI.SetActive(false);
    }

    public void QuitGame()
    {
        transition.SetActive(true);
        animator.SetTrigger("TopToBotB");
        StartCoroutine(ShutDown());
    }



    public void TryAgain()
    {
       // gameOverMenueUI.SetActive(false);
        transition.SetActive(true);
        animator.SetTrigger("TopToBotB");
        StartCoroutine(ReloadScene());
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
    }



    //corutines

    IEnumerator ActivateScene()
    {
        yield return new WaitForSeconds(0.4f);
        transition.SetActive(false);
    }

    /*
    IEnumerator ActivateMenue()
    {
        yield return new WaitForSeconds(0.4f);
        // transition.SetActive(false);
        // Resume();
        Resume();
        SceneManager.LoadScene("MainMenue");
    }
    */

    IEnumerator ShutDown()
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(0.4f);
        Application.Quit();
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        gameOverMenueUI.SetActive(true);
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

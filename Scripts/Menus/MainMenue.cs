using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenue : MonoBehaviour
{
    public GameObject mainMenueUI;
    public GameObject optionsMenueUI;
    public GameObject transition;
    public Animator animator;
    public AudioMixer audioMixer;

    public Slider slider;


    public void Awake()
    {
        transition.SetActive(true);
        animator.SetTrigger("TopToBotW");
        StartCoroutine(ActivateMenue());

        PlayerPrefs.SetInt("attack", 1);
        PlayerPrefs.SetInt("health", 6);
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetInt("fullHealth", 6);
        PlayerPrefs.SetInt("dash", 0);
        
        if(PlayerPrefs.GetFloat("volume") != null)
        {
            AudioListener.volume = PlayerPrefs.GetFloat("volume");
            slider.value = PlayerPrefs.GetFloat("volume");
        }
        else
        {
            AudioListener.volume = 0.5f;
            slider.value = 0.5f;
        }
        

    }

    public void LoadTutorial()
    {
        transition.SetActive(true);
        animator.SetTrigger("TopToBotB");
        StartCoroutine(WaitTransition("Tutorial"));
        //SceneManager.LoadScene("Tutorial");
    }

    public void LoadSettings()
    {
        mainMenueUI.SetActive(false);
        optionsMenueUI.SetActive(true);
    }

    public void BackButton()
    {
        mainMenueUI.SetActive(true);
        optionsMenueUI.SetActive(false);
    }

    public void QuitGame()
    {
        transition.SetActive(true);
        animator.SetTrigger("TopToBotB");
        StartCoroutine(ShutDown());
    }


    public void PlayGame()
    {
        transition.SetActive(true);
        animator.SetTrigger("TopToBotB");
        StartCoroutine(WaitTransition("Woods"));
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
    }

    //corutines

    IEnumerator WaitTransition(string str){
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(str);
    }

    IEnumerator ActivateMenue()
    {
        yield return new WaitForSeconds(0.4f);
        transition.SetActive(false);
    }

    IEnumerator ShutDown()
    {
        yield return new WaitForSeconds(0.4f);
        Application.Quit();
    }

}

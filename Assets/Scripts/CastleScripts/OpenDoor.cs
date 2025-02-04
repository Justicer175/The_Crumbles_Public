using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{

    [Header("Shadows")]
    [SerializeField] private GameObject shadow1;
    [SerializeField] private GameObject shadow2;
    
    [Header("Transition")]
    [SerializeField] private GameObject transition;

    [Header("Text")]
    [SerializeField] private GameObject information;
    [SerializeField] private TextMeshProUGUI tekst;

    [SerializeField] private GameObject player;

    private bool show = true;
    private bool uso = false;


    // Update is called once per frame
    void Update()
    {
        if(uso && Input.GetKeyDown("e"))
        {
            if(!shadow1.active && !shadow2.active) {
                StartCoroutine(loadLevel4());
            }
            else
            {
                if (show)
                {
                    StartCoroutine(showmESSAGE());
                }


            }
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            uso = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            uso = false;
        }
    }

    private IEnumerator loadLevel4()
    {
        transition.SetActive(true);
        transition.GetComponent<Animator>().SetTrigger("TopToBotB");


        PlayerPrefs.SetInt("attack", (int)player.GetComponent<Attack>().attackPower);
        PlayerPrefs.SetInt("health", (int)player.GetComponent<Health>().currentHp);
        PlayerPrefs.SetInt("score", player.GetComponent<Score>().scoreValue);
        PlayerPrefs.SetInt("fullHealth", (int)player.GetComponent<Health>().startingHp);

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("FinallBoss");
    }

    private IEnumerator showmESSAGE()
    {
        tekst.text = "Clear the enemies before entering the door!!";
        information.SetActive(true);
        show = false;
        yield return new WaitForSeconds(5f);
        show = true;
        information.SetActive(false);
    }
}

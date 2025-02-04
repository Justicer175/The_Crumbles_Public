using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartFinalBoss : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] GameObject player;

    [Header ("Boss")]
    [SerializeField] Animator animator;

    [Header("Info")]
    [SerializeField] private GameObject blackArea;
    [SerializeField] private TextMeshProUGUI tekst;

    


    private bool waiting = true;
    private float timeToTalk = 10f;
    private int line = 0;

    private bool end = false;
    private int newLine = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitforSpeach());
        //timeToTalk = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!waiting)
        {
            timeToTalk -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.E) || timeToTalk < 0 && line < 6)
            {
                line++;
                timeToTalk = 10f;
            }
            if (line == 0)
            {
                blackArea.SetActive(true);
                tekst.text = "What!?!?";
            }
            else if (line == 1)
            {
                tekst.text = "How did you make it here?";
            }
            else if (line == 2)
            {
                tekst.text = "Aaaaaahhhhhh!!";
            }
            else if (line == 3)
            {
                tekst.text = "I guess you have to do everything by yourself these days.";
            }
            else if (line == 4)
            {
                tekst.text = "Lets have it then!!!!";
            }
            else if (line == 5)
            {
                blackArea.SetActive(false);
                player.GetComponent<Movement>().enabled = true;
                player.GetComponent<Attack>().enabled = true;
                animator.SetTrigger("startBoss");
                waiting = true;
            }
            
        }
        else if (end)
        {
            
         
            
            //timeToTalk -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.E) || timeToTalk < 0 && newLine < 8)
            {
                newLine++;
                timeToTalk = 10f;
            }

            if (newLine == 0)
            {
                animator.speed = 0;
                blackArea.SetActive(true);
                tekst.text = "I cant beleive it!";
            }
            else if (newLine == 1)
            {
                tekst.text = "*CoUgH* *CoUgH*";
            }
            else if (newLine == 2)
            {
                tekst.text = "You actually managed to defeat me.";
            }
            else if (newLine == 3)
            {
                tekst.text = "*CoUgH* *CoUgH*";
            }
            else if (newLine == 4)
            {
                tekst.text = "Well done.";
            }
            else if (newLine == 5)
            {
                tekst.text = "This will not be the last of me";

            }
            else if (newLine == 6)
            {
                tekst.text = "Untill next time!";
            }
            else if (newLine == 7)
            {
                blackArea.SetActive(false);
                animator.SetTrigger("dissapear1");
                animator.speed = 1;
                StartCoroutine(waittoDestroy());
                //animator.SetTrigger("startBoss");
            }

        }
    }

    public void endFinalBoss()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Static;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        end = true;
    }
    

    IEnumerator waitforSpeach()
    {
        yield return new WaitForSeconds(0.3f);
        waiting = false;
    }

    IEnumerator waittoDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        
    }

    
}

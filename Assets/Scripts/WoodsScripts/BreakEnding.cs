using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakEnding : MonoBehaviour
{

    private bool ending;
    private Rigidbody2D pRB;
    private Vector2 velocity;
    private Animator anim;
    private bool talk;
    private float timeToTalk = 10f;
    private int line = 0;
    //[SerializeField] private float speed = 7f;

    [Header ("Player")]
    [SerializeField] private GameObject player;

    [Header("Wizard")]
    [SerializeField] private Animator animWizard;

    [Header("Text")]
    [SerializeField] private GameObject information;
    [SerializeField] private TextMeshProUGUI tekst;

    [Header("Floor")]
    [SerializeField] private GameObject floor;
    //[SerializeField] GameObject camera;

    [Header("Transition")]
    [SerializeField] private GameObject transition;



    private void Start()
    {
        pRB = player.GetComponent<Rigidbody2D>();
        anim = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ending)
        {



            if (!talk)
            {
                if (player.transform.position.x >= 279f)
                {
                    anim.SetInteger("state", 0);
                    pRB.velocity = Vector2.zero;
                    talk = true;
                    timeToTalk = 10f;
                }
                else
                {
                    pRB.velocity = velocity;


                }

                if(talk)
                {
                    StartCoroutine(enableTalk());
                }
            }
            else
            {
                timeToTalk -= Time.deltaTime; 
                if (Input.GetKeyDown(KeyCode.E) || timeToTalk < 0 && line < 8)
                {
                    line++;
                    timeToTalk = 10f;
                }
                    if(line == 0)
                    {
                        tekst.text = "I see you have made it through the jungle.";
                    }
                    else if(line == 1)
                    {
                        tekst.text = "Impressive.";
                    }
                    else if(line == 2)
                    {
                        tekst.text = "But you didn't think it would be that easy to get to the castle.";
                    }
                    else if (line == 3)
                    {
                        tekst.text = "Ough. No, no no!";
                    }
                    else if (line == 4)
                    {
                        tekst.text = "I have no time for the like's of you.";
                    }
                    else if(line == 5)
                    {
                        tekst.text = "Be gone fool!!!";
                        animWizard.SetBool("break", true);
                    }
                    else if(line == 6)
                    {
                        information.SetActive(false);
                        anim.SetInteger("state", 3);
                        floor.SetActive(false);
                    }
                    else if (line == 7)
                    {
                        animWizard.SetBool("break", false);
                        information.SetActive(true);
                        tekst.text = "Let's see if she gets out of this one.";
                    }
                    else if (line == 8)
                    {
                    //Transition
                        StartCoroutine(loadLevel2());
                    }
                
            }
            

            //Debug.Log(pRB.velocity);


            

           // player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(279f, -4.637978f), speed * Time.deltaTime);

            //rigidbody.velocity = new Vector2(pRB.velocity.x, rigidbody.velocity.y);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            ending = true;
            player.GetComponent<Movement>().enabled = false;
            velocity = pRB.velocity
;        }
    }

    //corutines

    private IEnumerator enableTalk()
    {
        yield return new WaitForSeconds(2f);
        information.SetActive(true);
        
    }

    private IEnumerator loadLevel2()
    {
        transition.SetActive(true);
        transition.GetComponent<Animator>().SetTrigger("TopToBotB");

        

        yield return new WaitForSeconds(0.4f);

        PlayerPrefs.SetInt("attack", (int)player.GetComponent<Attack>().attackPower);
        PlayerPrefs.SetInt("health", (int)player.GetComponent<Health>().currentHp);
        PlayerPrefs.SetInt("score", player.GetComponent<Score>().scoreValue);
       // Debug.Log(PlayerPrefs.GetInt("score"));
        PlayerPrefs.SetInt("fullHealth", (int)player.GetComponent<Health>().startingHp);
        PlayerPrefs.SetInt("dash", 1);

        SceneManager.LoadScene("Mines");
    }

}

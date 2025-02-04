using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{

    [Header("Player")]
    [SerializeField] private GameObject player;

    [Header("Transition")]
    [SerializeField] private GameObject transition;

    [Header("Boss")]
    [SerializeField] GameObject boss;

    [Header("Ladder")]
    [SerializeField] Transform ladder;
    [SerializeField] Vector2 position;
    [SerializeField] float speed;

    private bool touched = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!boss.active)
        {
            ladder.position = Vector2.MoveTowards(ladder.position, position, speed * Time.deltaTime);
            if (touched && Input.GetKeyDown("w"))
            {
                StartCoroutine(loadLevel3());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" ) { 
            touched = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            touched = false;
        }
    }

    private IEnumerator loadLevel3()
    {
        transition.SetActive(true);
        transition.GetComponent<Animator>().SetTrigger("TopToBotB");

        PlayerPrefs.SetInt("attack", (int)player.GetComponent<Attack>().attackPower);
        PlayerPrefs.SetInt("health", (int)player.GetComponent<Health>().currentHp);
        PlayerPrefs.SetInt("score", player.GetComponent<Score>().scoreValue);
        PlayerPrefs.SetInt("fullHealth", (int)player.GetComponent<Health>().startingHp);

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Castle");
    }
}

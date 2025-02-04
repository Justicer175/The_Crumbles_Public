using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTransition : MonoBehaviour
{
    [SerializeField] private GameObject transition;
    [SerializeField] private GameObject boss;

    // Update is called once per frame
    void Update()
    {
        if (!boss.active)
        {
            StartCoroutine(endTheGame());
        }
    }

    IEnumerator endTheGame()
    {
        //Debug.Log("STARTO");
        yield return new WaitForSeconds(1f);
        transition.SetActive(true);
        transition.GetComponent<Animator>().SetTrigger("TopToBotB");
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("TheEnd");
    }
}

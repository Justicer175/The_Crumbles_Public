using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;




public class StartBoss : MonoBehaviour
{

    [Header("Cameras")]
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject boosCamera;

    [Header("Name")]
    [SerializeField] private GameObject blackArea;
    [SerializeField] private TextMeshProUGUI whiteArea;

    [Header("Boss")]
    [SerializeField] private GameObject boss;
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            mainCamera.SetActive(false);
            boosCamera.SetActive(true);
            StartCoroutine(waitforBoss());
        }
    }

    IEnumerator waitforBoss()
    {
        whiteArea.text = "The fallen hero";
        blackArea.SetActive(true);
        yield return new WaitForSeconds(2f);
        blackArea.SetActive(false );
        animator.SetTrigger("startBoss");
    }
}

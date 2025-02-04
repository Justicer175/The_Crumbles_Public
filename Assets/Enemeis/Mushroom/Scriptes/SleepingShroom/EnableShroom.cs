using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableShroom : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private GameObject objekt;

    [Header("To check if dead")]
    [SerializeField] private GameObject shroom1;
    [SerializeField] private GameObject shroom2;

    [Header("Animation")]
    [SerializeField] private Animator anim;


    private void Update()
    {
        /*
        Debug.Log(shroom1.activeInHierarchy);
        Debug.Log("Shroom2: "+ shroom2.activeInHierarchy);
        */
        if (!shroom1.activeInHierarchy && !shroom2.activeInHierarchy)
        {
            anim.SetTrigger("wakeUp");
        }
    }
    public void enable()
    {
        objekt.SetActive(true);
        gameObject.SetActive(false);
    }
}

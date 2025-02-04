using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterShop : MonoBehaviour
{
    //private bool entered = false;

    [SerializeField] OpenShop OpenShop;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("yes");
        if (collision.tag == "Player")
        {
            //Debug.Log("lol");
            //entered = true;
            OpenShop.entered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //entered = false;
            OpenShop.entered = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeCamera : MonoBehaviour
{
    public bool freezeCamera = false;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private GameObject breakTheWall;

    private void Update()
    {
        int i = 0;
        foreach (GameObject go in enemies)
        {
            if (!go.activeInHierarchy)
            {
                i++;
            }
        }
        if(i == 6)
        {
            freezeCamera = false;   
            breakTheWall.SetActive(false);
        }

    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            freezeCamera = true;
        }
        
    }
    */

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            freezeCamera = true;
        }
    }
    
    
}

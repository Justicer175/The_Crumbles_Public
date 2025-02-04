using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnOnAcid : MonoBehaviour
{

    [SerializeField] private Movement move;
    [SerializeField] private Health health;
    private Vector3 position;
    private bool inAcid;
    private bool waiting;
    private Movement movement;

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }


    void Update()
    {
        
        /*
        if (move.Grounded() && !waiting)
        {
            position = gameObject.transform.position;
            StartCoroutine(wait());
            Debug.Log(position);
        }
        */

        if (inAcid)
        {
            gameObject.transform.position = position;
            health.TakeDamage(1);
            inAcid = false;
            movement.isClimbingLadder = false;
        }
    }

    /*
    IEnumerator wait()
    {
        waiting = true;
        yield return new WaitForSeconds(1f);
        waiting = false;
    }
    */


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ReturnPoint"))
        {
            position = collision.gameObject.transform.position;
        }


        if (collision.CompareTag("Acid"))
        {
            //Debug.Log("in");
            
            inAcid = true;
        }
    }

    

}

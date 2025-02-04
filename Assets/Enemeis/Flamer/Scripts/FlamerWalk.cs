using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamerWalk : MonoBehaviour
{
    [Header("Area")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft = true;


    [Header("Animations")]
    [SerializeField] private Animator anim;

    /*
    [Header("Player to follow")]
    [SerializeField] private Transform player;
    [SerializeField] private Detected detected;
    */

    private void Awake()
    {
        initScale = enemy.localScale;
        //detected = gameObject.GetComponent<Detected>();
    }



    private void Update()
    {



        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                ChangeDirection();
            }
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                ChangeDirection();
            }
        }





    }



    private void ChangeDirection()
    {


        movingLeft = !movingLeft;


    }

    private void MoveInDirection(int direction)
    {

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * -direction, initScale.y, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed,
            enemy.position.y, enemy.position.z);
    }
}

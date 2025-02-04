using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFly : MonoBehaviour
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

    [Header("Idle Behavior")]
    [SerializeField] private float idleTime;
    private float idleTimer;

    [Header("Animations")]
    [SerializeField] private Animator anim;

    [Header("Player to follow")]
    [SerializeField] private Transform player;
    [SerializeField] private Detected detected;

    private Vector3 locationOfEye;

    private void Awake()
    {
        initScale = enemy.localScale;
        //detected = gameObject.GetComponent<Detected>();
        locationOfEye = enemy.position;
    }

    private void OnDisable()
    {
        anim.SetBool("running", false);
    }

    private void Update()
    {

        if( Mathf.Abs(enemy.position.x - player.position.x) > 10f){
            detected.detected = false;
        }

        
            if (detected.detected)
            {
                if (enemy.position.x > player.position.x)
                {
                    FollowPlayer(-1);
                }
                else
                {
                    movingLeft = false;
                    FollowPlayer(1);
                }
            }
            else if (enemy.position.y != leftEdge.position.y || enemy.position.x > rightEdge.position.x + 1 || enemy.position.x < leftEdge.position.x - 1)
            {
            //Debug.Log(enemy.localScale.x);
                
                if (enemy.position.x < locationOfEye.x)
                {
                    movingLeft = false;
                    ReturnHome(1);
                }
                else
                {
                    
                    ReturnHome(-1);
                }
            }
            else
            {
                detected.detected = false;
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
            

        
            /*
        else
        {
            detected.detected = false;
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
            */




    }



    private void ChangeDirection()
    {
        

        idleTimer += Time.deltaTime;


        if (idleTimer > idleTime)
        {
            movingLeft = !movingLeft;
        }

    }

    private void MoveInDirection(int direction)
    {
        idleTimer = 0;
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * -direction, initScale.y, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed,
            enemy.position.y, enemy.position.z);
    }

    private void FollowPlayer(int direction)
    {
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * -direction, initScale.y, initScale.z);
        enemy.position = Vector3.MoveTowards(enemy.position, new Vector3(player.position.x,
            player.position.y, enemy.position.z), Time.deltaTime * speed);
        /*
        enemy.position = new Vector3(player.position.x + Time.deltaTime  * speed,
            player.position.y, enemy.position.z);
        */
    }

    private void ReturnHome(int direction)
    {
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * -direction, initScale.y, initScale.z);
        enemy.position = Vector3.MoveTowards(enemy.position, locationOfEye, Time.deltaTime * speed);
    }
}

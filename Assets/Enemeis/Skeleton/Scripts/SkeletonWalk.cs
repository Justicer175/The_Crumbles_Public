using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWalk: MonoBehaviour
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

    private void Awake()
    {
        initScale = enemy.localScale;
        //detected = gameObject.GetComponent<Detected>();
        if (initScale.x < 0)
        {
            movingLeft = false;
        }
    }

    private void OnDisable()
    {
        anim.SetBool("running", false);
    }

    private void Update()
    {

        if (player.position.x >= leftEdge.position.x && player.position.x <= rightEdge.position.x)
        {
            if (detected.detected)
            {
                if (enemy.position.x > player.position.x)
                {
                    MoveInDirection(-1);
                }
                else
                {
                    movingLeft = false;
                    MoveInDirection(1);
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




    }



    private void ChangeDirection()
    {
        anim.SetBool("running", false);

        idleTimer += Time.deltaTime;


        if (idleTimer > idleTime)
        {
            movingLeft = !movingLeft;
        }

    }

    private void MoveInDirection(int direction)
    {
        idleTimer = 0;
        anim.SetBool("running", true);
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * -direction, initScale.y, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed,
            enemy.position.y, enemy.position.z);
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack2FinalBoss : StateMachineBehaviour
{

    bool goLeft = true;
    Vector2 leftBorder = new Vector2(-24f,-8.09f);
    Vector2 rightBorder = new Vector2(24f,-8.09f);


    Transform bossRotation;
    Rigidbody2D rb;
    float speed = 5f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        bossRotation = animator.GetComponent<Transform>();
        if(bossRotation.localScale.z == -1f)
        {
            goLeft = false;
            
        }
        else
        {
            goLeft = true;
        }
       // Debug.Log(bossRotation.localScale.z);
       // Debug.Log(bossRotation.localRotation.y);
       // Debug.Log(goLeft);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(goLeft) {
            //Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPostiono = Vector2.MoveTowards(rb.position, leftBorder, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPostiono);

            if(animator.transform.position.x <= leftBorder.x +1)
            {
                animator.SetTrigger("endattack2");
            }
        }
        else
        {
            Vector2 newPostiono = Vector2.MoveTowards(rb.position, rightBorder, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPostiono);
            if (animator.transform.position.x >= rightBorder.x - 1)
            {
                animator.SetTrigger("endattack2");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // animator.ResetTrigger("endattack2");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

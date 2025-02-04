using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_run : StateMachineBehaviour
{
    public float speed = 5f;
    Transform player;
    Rigidbody2D rb;
    TurnBoss turnBoss;

    float attackRange = 3f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        turnBoss = animator.GetComponent<TurnBoss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        turnBoss.LookAtPlayer();

        Vector2 target = new Vector2(player.position.x,rb.position.y);
        Vector2 newPostiono = Vector2.MoveTowards(rb.position, target,speed * Time.fixedDeltaTime);
        rb.MovePosition(newPostiono);

       if(Vector2.Distance(player.position,rb.position) <= attackRange)
        {
            if(Random.Range(0,2) == 0)
            {
                animator.SetTrigger("attack1");
            }
            else
            {
                animator.SetTrigger("attack2");
            }
            
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack1");
        animator.ResetTrigger("attack2");
    }

   
}

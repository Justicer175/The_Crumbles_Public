using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public Animator anim;
    public Transform attackPointRight;
    public Transform attackPointLeft;
    public float range = 0.5f;
    public LayerMask enemies;

    private BoxCollider2D coll;
    [SerializeField] private LayerMask ground;

    public bool attacking = false;
    public float attackPower = 1;


    private void Awake()
    {
        if(gameObject.tag == "Player")
        {
            attackPower = PlayerPrefs.GetInt("attack");
           
        }
    }

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();

    }

    
    void Update()
    {
        
        if(!PauseMenue.GamePaused)
        {



            if (Input.GetMouseButtonDown(0) && !attacking && Grounded())
            {
                //Debug.Log("uso");
                attacking = true;
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                //StartCoroutine(FullAttack());
                anim.SetTrigger("Attack");
            }



        }

        
    }

    public void doneAttacking()
    {
        attacking = false;
    }

    public void Atk()
    {
       // anim.SetTrigger("Attack");

        Collider2D[] hit;

        

        if (!gameObject.GetComponent<SpriteRenderer>().flipX)
        {
            hit = Physics2D.OverlapCircleAll(attackPointRight.position, range, enemies);
        }
        else
        {
            hit = Physics2D.OverlapCircleAll(attackPointLeft.position, range, enemies);
        }
       // Collider2D[] hit = Physics2D.OverlapCircleAll(attackPointRight.position,range,enemies);

        if(hit != null)
        {
            foreach (Collider2D enemy in hit)
            {
                if (enemy.CompareTag("Enemy"))
                {

                    if(enemy.name == "Boss")
                    {
                        BossHealth enemyScript = enemy.GetComponent<BossHealth>();
                        if (!enemyScript.invoulnerable)
                        {
                            enemyScript.TakeDamage(attackPower);
                        }
                    }
                    else
                    {
                        Health enemyScript = enemy.GetComponent<Health>();
                        if (!enemyScript.invoulnerable)
                        {
                            enemyScript.TakeDamage(attackPower);
                        }
                    }


                    
                    
                }
                else if (enemy.CompareTag("BreakableWall"))
                {
                    //Debug.Log(enemy.GetComponent<Transform>());
                    //enemy.enabled = false;
                    enemy.gameObject.SetActive(false);
                    /*
                    GameObject tmp =  enemy.GetComponentInParent<GameObject>();
                    tmp.SetActive(false);
                    */
                    
                }
               // Debug.Log(enemy);
                //if(enemy.GetComponent<Health>() != null)
                    
            }

        } 
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(attackPointRight.position,range);

        Gizmos.DrawSphere(attackPointLeft.position, range);

        
    }

    private bool Grounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, ground);
    }

    //corutines

    /*
    IEnumerator FullAttack()
    {
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        Atk();
        yield return new WaitForSeconds(0.4f);
        Atk();
        yield return new WaitForSeconds(0.3f);
        attacking = false;
    }
    */

    
}

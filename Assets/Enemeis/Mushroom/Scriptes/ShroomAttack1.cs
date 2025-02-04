using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomAttack1 : MonoBehaviour
{
    [SerializeField] private float attackCd;
    [SerializeField] private float range;
    [SerializeField] private float distance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask mask;
    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
    private Health playerHealth;

    private ShroomWalk patrol;

    private bool attacking;

    private void Start()
    {
        anim = GetComponent<Animator>();
        patrol = GetComponentInParent<ShroomWalk>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;


        if (InRange())
        {
            if (cooldownTimer >= attackCd)
            {
                StartCoroutine(WaitToWalk());
                cooldownTimer = 0;
                anim.SetTrigger("attack1");
               
            }
            
        }

        if(patrol != null)
        {
            if(attacking || InRange())
            {
                patrol.enabled = false;
            }
            else
            {
                patrol.enabled=true;
            }
             //patrol.enabled = !InRange();
        }
        
    }

    private bool InRange()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * distance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, mask);

        if (hit.collider != null) {
            playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * distance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));

    }

    private void DamagePlayer()
    {
        if(InRange())
        {
            playerHealth.TakeDamage(damage);
        }
    }

    //corutines

    private IEnumerator WaitToWalk()
    {
        attacking = true;
        yield return new WaitForSeconds(attackCd/1.5f);
        attacking = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    private SpriteRenderer sprite;
    private Animator anim;
    private BoxCollider2D coll;

    [SerializeField] private LayerMask ground;

    private float dirX ;
    private enum MovementStates {idle, running, jumping, falling,cLadder};

    //Movement

    private float movementSpeed = 7f;
    private float fallSpeed = 17f;

    //Ladder

    private float dirY;
    private float speedLadder = 7f;
    private bool isLadder;
    public bool isClimbingLadder;

    private float ladderCoordinateX;

    private bool jumping = false;


    //attacking

    private Attack attackScript;

    //dashing

    private bool canDash = true;
    private bool isDash = false;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCD = 1f;
    private Health health;

    public int dashaquired = 0;
    private bool haveDash = true;

    private float invulnerable = 0.5f;
    private float numberOfFlashes = 2f;

    //awake

    private void Awake()
    {
        dashaquired = PlayerPrefs.GetInt("dash");
    }


    //Start

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        attackScript = GetComponent<Attack>();
        health = GetComponent<Health>();
    }


    void Update()
    {

        

        if (!PauseMenue.GamePaused)
        {
            if (isDash)
            {
                return;
            }

            if (Grounded())
            {
                haveDash = true;
            }


            dirX = Input.GetAxisRaw("Horizontal");
            dirY = Input.GetAxis("Vertical");

           // Debug.Log(dirX * movementSpeed);

            if (isLadder && (dirY != 0 || isClimbingLadder) && !jumping)
            {
                rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
                isClimbingLadder = true;
            }
            else
            {
                rigidbody.constraints = RigidbodyConstraints2D.None;
                rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                isClimbingLadder = false;
            }


            if (Input.GetButtonDown("Jump") && (Grounded() || isClimbingLadder) && !attackScript.attacking)
            {
                //Debug.Log(jumping);
                rigidbody.velocity = new Vector2(dirX * movementSpeed, fallSpeed);
                isClimbingLadder = false;
                jumping = true;
                StartCoroutine(JumpFromLadder());

            }


            if (isClimbingLadder)
            {
                rigidbody.gravityScale = 0f;
                rigidbody.position = new Vector2(ladderCoordinateX, rigidbody.position.y);
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, dirY * speedLadder);
            }
            else
            {
                rigidbody.gravityScale = 3f;
            }

            if (Input.GetMouseButtonDown(1) && canDash && dashaquired == 1 && haveDash)
            {
                 //Debug.Log("lmao");
                //change da nemas to neg samo get
                //dodaj da kad stoji se moze dashat
                haveDash = false;
                StartCoroutine(Dash());
                StartCoroutine(Invulnerability());
                
            }


            //rigidbody.velocity = new Vector2(dirX * movementSpeed, rigidbody.velocity.y);

            // rigidbody.MovePosition(new Vector2(dirX * movementSpeed, rigidbody.velocity.y));




            Animations();
        }


        
    }

    private void FixedUpdate()
    {
        if (!PauseMenue.GamePaused)
        {
            if (isDash)
            {
                return;
            }

            rigidbody.velocity = new Vector2(dirX * movementSpeed, rigidbody.velocity.y);
        }
    }

    private void Animations()
    {
        MovementStates state;

        if (isDash)
        {
            anim.SetTrigger("dash");
        }

        if (dirX > 0f)
        {
            state = MovementStates.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementStates.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementStates.idle;
        }


        if (isClimbingLadder)
        {
            state = MovementStates.cLadder;
           // Debug.Log(dirY);
            if (dirY == 0f)
            {
                anim.SetFloat("SpeedOfLadder", 0f);
            }
            else if (dirY != 0f)
            {
                anim.SetFloat("SpeedOfLadder", 1f);
            }
        }
        else if (rigidbody.velocity.y > .01f)
        {
            state = MovementStates.jumping;
        }
        else if (rigidbody.velocity.y < -.01f)
        {
            state = MovementStates.falling;
        }

        



        anim.SetInteger("state",(int)state);
    }

    public bool Grounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, ground);
    }

    //Ladder

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            //Debug.Log("Enter");
            isLadder = true;

           Transform tmp = collision.GetComponent<Transform>();
           ladderCoordinateX = tmp.position.x;
           // Debug.Log(ladderCoordinateX);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Ladder"))
        {
           // Debug.Log("Left");
            isLadder = false;
        }
    }


    //coruutines

    IEnumerator JumpFromLadder()
    {

        yield return new WaitForSeconds(0.3f);
        //Debug.Log("waited");
        jumping = false;
        yield return null;
    }

    IEnumerator Dash()
    {

        canDash = false;
        isDash = true;
        float originalGravity = rigidbody.gravityScale;
        rigidbody.gravityScale = 0f;
        if (!sprite.flipX)
        {
            rigidbody.velocity = new Vector2(dashingPower * transform.localScale.x, 0f);
        }
        else
        {
            rigidbody.velocity = new Vector2(-dashingPower * transform.localScale.x, 0f);
        }
        //rigidbody.velocity = new Vector2(dirX * dashingPower, 0f);

        yield return new WaitForSeconds(dashingTime);
        rigidbody.gravityScale = originalGravity;
        isDash = false;
        yield return new WaitForSeconds(dashingCD);
        canDash = true;
    }

    public IEnumerator Invulnerability()
    {
        health.invoulnerable = true;
        Physics2D.IgnoreLayerCollision(9, 8, true);

        for (int i = 0; i < numberOfFlashes; i++)
        {
            sprite.color = new Color(150, 150, 150, 0.5f);
            yield return new WaitForSeconds(invulnerable / (numberOfFlashes * 2));
            sprite.color = Color.white;
            yield return new WaitForSeconds(invulnerable / (numberOfFlashes * 2));
        }


        //yield return new WaitForSeconds(invulnerable);

        Physics2D.IgnoreLayerCollision(9, 8, false);
        health.invoulnerable = false;
    }


}

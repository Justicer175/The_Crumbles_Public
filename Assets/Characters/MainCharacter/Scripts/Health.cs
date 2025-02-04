using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{


    [SerializeField] public float startingHp;
    public float currentHp { get;  set; }
    private Animator anim;
    public bool dead;

    [Header("iFrames")]
    [SerializeField] private float invulnerable;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRenderer;
    public bool invoulnerable;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    [Header("For plyer")]
    [SerializeField] private Attack atack;

    [Header("ForEnemie")]
    [SerializeField] private int value = 0;

    [Header("Score")]
    [SerializeField] private Score score;


    private void Awake()
    {
        currentHp = startingHp;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();


        if(gameObject.tag == "Player")
        {
            currentHp = PlayerPrefs.GetInt("health");
            startingHp = PlayerPrefs.GetInt("fullHealth");
        }
    }

    

    public void TakeDamage(float dmg)
    {
        //Debug.Log("hit");
        currentHp = Mathf.Clamp(currentHp - dmg, 0, startingHp);

        if(gameObject.tag == "Player")
        {
            atack.doneAttacking();
        }

        if(currentHp >0) {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else
        {
            if(!dead)
            {

                if(gameObject.tag == "Enemy")
                {
                    score.scoreValue += value;
                }

                anim.SetTrigger("dead");


                foreach(Behaviour comp in components)
                {
                    comp.enabled = false;
                }
                dead = true;
            }
            
        }
    }

    public IEnumerator Invulnerability()
    {
        invoulnerable = true;
        Physics2D.IgnoreLayerCollision(9, 8,true);

        if(gameObject.tag == "Enemy" && gameObject.name == "ShroomEnemy" || gameObject.name == "Skeleton" ||gameObject.name == "BringerOfDeath")
        {
            //Debug.Log (gameObject.name);
           // Debug.Log("Uso");
            Detected detect = GetComponentInChildren<Detected>();
            detect.detected = true;
        }
        
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(0, 30, 100, 0.7f);
            yield return new WaitForSeconds(invulnerable/ (numberOfFlashes * 2) );
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(invulnerable / (numberOfFlashes * 2));
        }
        

        //yield return new WaitForSeconds(invulnerable);

       Physics2D.IgnoreLayerCollision(9, 8, false);
        invoulnerable = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

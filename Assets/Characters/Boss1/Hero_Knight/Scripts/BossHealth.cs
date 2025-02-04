using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private float startingHp;
    public float currentHp { get; private set; }
    private Animator anim;
    public bool dead;
    public float phase = 0;

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


    [Header("SleepingSkeletonsPhase1")]
    [SerializeField] List<GameObject> skelis1;

    [Header("SleepingSkeletonsPhase2")]
    [SerializeField] List<GameObject> skelis2;

    


    private void Awake()
    {
        currentHp = startingHp;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }



    public void TakeDamage(float dmg)
    {
        //Debug.Log("hit");
        currentHp = Mathf.Clamp(currentHp - dmg, 0, startingHp);


        if (currentHp > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());

            if (currentHp < startingHp*0.67 && phase == 0)
            {
                foreach(GameObject go in skelis1)
                {
                    go.GetComponent<Animator>().SetTrigger("rise");
                }


                anim.SetTrigger("jump");
                anim.SetTrigger("dissapear");
                phase = 0.5f;
            }
            else if(currentHp < startingHp*0.34 && phase == 1)
            {
                foreach (GameObject go in skelis2)
                {
                    go.GetComponent<Animator>().SetTrigger("rise");
                }

                anim.SetTrigger("jump");
                anim.SetTrigger("dissapear");
                phase = 1.5f;
            }

            

        }
        else
        {
            if (!dead)
            {

                if (gameObject.tag == "Enemy")
                {
                    score.scoreValue += value;
                }

                anim.SetTrigger("dead");


                foreach (Behaviour comp in components)
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
        Physics2D.IgnoreLayerCollision(9, 8, true);

        

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(0, 30, 100, 0.7f);
            yield return new WaitForSeconds(invulnerable / (numberOfFlashes * 2));
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

    public void dissapear()
    {
        gameObject.transform.position = new Vector2(0,-18);
    }

    public void appear()
    {
        gameObject.transform.position = new Vector2(23.63f, -7.8f);
    }

    
}

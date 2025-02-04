using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseAssist : MonoBehaviour
{

    private bool waiting = true;
    private bool firstWait = true;
    private BossHealth health;

    [Header("AwakeSkeletonsPhase1")]
    [SerializeField] List<GameObject> skelis1;

    [Header("AwakeSkeletonsPhase2")]
    [SerializeField] List<GameObject> skelis2;

    // Start is called before the first frame update
    void Start()
    {
        health = gameObject.GetComponent<BossHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health.phase == 0.5)
        {
            if (firstWait)
            {
                StartCoroutine(wait());
            }

            if(!waiting) {
                int i = 0;
                foreach (GameObject go in skelis1)
                {
                    if (!go.active)
                    {
                        i++;
                    }

                    if (i == 2)
                    {
                        health.phase = 1;
                        gameObject.GetComponent<Animator>().SetTrigger("fall");
                        gameObject.GetComponent<Animator>().SetTrigger("appear");
                        firstWait = true;
                        waiting = true;
                    }
                }
            }
            
            
        }
        else if(health.phase == 1.5)
        {
            if (firstWait)
            {
                StartCoroutine(wait());
            }
            if (!waiting)
            {
                int i = 0;
                foreach (GameObject go in skelis2)
                {
                    if (!go.active)
                    {
                        i++;
                    }

                    if (i == 2)
                    {
                        health.phase = 2;
                        gameObject.GetComponent<Animator>().SetTrigger("fall");
                        gameObject.GetComponent<Animator>().SetTrigger("appear");
                        firstWait = false;
                        waiting = true;
                    }
                }
            }
        }
    }

    IEnumerator wait()
    {
        firstWait = false;
        yield return new WaitForSeconds(2f);
        waiting = false;
    }
}

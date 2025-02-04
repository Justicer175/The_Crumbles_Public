using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseSkeleton : MonoBehaviour
{
    [SerializeField] GameObject skelington;

    public void summon()
    {
        skelington.SetActive(true);
        skelington.GetComponentInChildren<Detected>().detected = true;
        gameObject.SetActive(false);

    }


}

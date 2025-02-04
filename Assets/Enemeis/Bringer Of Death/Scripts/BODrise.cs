using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BODrise : MonoBehaviour
{
    [SerializeField] GameObject BOD;

    public void summon()
    {
        BOD.SetActive(true);
        BOD.GetComponentInChildren<Detected>().detected = true;
        gameObject.SetActive(false);

    }
}

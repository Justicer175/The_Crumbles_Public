using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform player;    
    

    // Update is called once per frame
    void Update()
    {
        if(player.position.x > gameObject.transform.position.x)
        {
            gameObject.transform.localScale = new Vector3(1f, gameObject.transform.lossyScale.y, gameObject.transform.lossyScale.z);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-1f, gameObject.transform.lossyScale.y, gameObject.transform.lossyScale.z);
        }
    }
}

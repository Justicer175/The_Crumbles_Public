using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WoodsInfo : MonoBehaviour
{
    [SerializeField] private GameObject blackArea;
    [SerializeField] private TextMeshProUGUI whiteArea;
   // [SerializeField] private PauseMenue menue;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "Sign1")
        {
            blackArea.SetActive(true);
            whiteArea.text = "Press E to interact with people.";
        }
        else if (collision.gameObject.name == "Sign2")
        {
            blackArea.SetActive(true);
            whiteArea.text = "Dash with Right click";
            gameObject.GetComponent<Movement>().dashaquired = 1;
            
        }
        
        //Debug.Log(collision.gameObject.name);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Sign")
        {
            blackArea.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Unity.UI;
using UnityEngine.UI;
using TMPro;

public class Tutorialnformation : MonoBehaviour
{
    [SerializeField] private GameObject blackArea;
    [SerializeField] private TextMeshProUGUI whiteArea;
    [SerializeField] private PauseMenue menue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "Sign1")
        {      
           blackArea.SetActive(true);
           whiteArea.text = "Move left with A and right with D.";
        }
        else if (collision.gameObject.name == "Sign2")
        {
            blackArea.SetActive(true);
            whiteArea.text = "Jump with space.";
        }
        else if (collision.gameObject.name == "Sign3")
        {
            blackArea.SetActive(true);
            whiteArea.text = "Attack with left mouse button.";
                
        }
        else if (collision.gameObject.name == "Sign4")
        {
            blackArea.SetActive(true);
            whiteArea.text = "Watch out for acid!";
        }
        else if (collision.gameObject.name == "Sign5")
        {
            blackArea.SetActive(true);
            whiteArea.text = "On your journy you will face a lot of enemies, some are less dangerous like this one...";
        }
        else if (collision.gameObject.name == "Sign6")
        {
            blackArea.SetActive(true);
            whiteArea.text = "Some enemies will fight you with everything they've got, so watch out!";
        }
        else if (collision.gameObject.name == "Sign7")
        {
            blackArea.SetActive(true);
            whiteArea.text = "Climb the ladder with W";
        }
        else if (collision.gameObject.name == "Sign(Exit)")
        {
            blackArea.SetActive(true);
            whiteArea.text = "EXIT";
        }
        else if (collision.gameObject.name == "Ending")
        {
            //Debug.Log("DONE");
            menue.LoadMenue();
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

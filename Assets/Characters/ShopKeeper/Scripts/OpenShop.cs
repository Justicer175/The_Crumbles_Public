using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenShop : MonoBehaviour
{

    public bool entered = false;
    
    [SerializeField] private GameObject shopMenue;
    [SerializeField] private TextMeshProUGUI ATKvalue;
    [SerializeField] private TextMeshProUGUI HPvalue;

    [SerializeField] private Health playerHealth;
    private float healthvalue;
    private int costHPvalue;
    [SerializeField] private TextMeshProUGUI costHP;
    [SerializeField] private Attack playerATK;
    private float atkvalue;
    private int costATKvalue;
    [SerializeField]private TextMeshProUGUI costATK;

    [SerializeField] Score score;
   // private float scorevalue;

    // Start is called before the first frame update
    void Start()
    {
       // atkvalue = playerATK.attackPower;
        //scorevalue = score.scoreValue;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (shopMenue.activeSelf)
        {
            ATKvalue.text = playerATK.attackPower.ToString();
            HPvalue.text = playerHealth.startingHp.ToString();

            Time.timeScale = 0f;
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
            {
                Time.timeScale = 1f;
                shopMenue.SetActive(false);
                PauseMenue.GamePaused = false;
            }

            if(playerATK.attackPower == 1)
            {
                costATK.text = "6";
                costATKvalue = 6;
            }
            else if(playerATK.attackPower == 2)
            {
                costATK.text = "12";
                costATKvalue = 12;
            }else if(playerATK.attackPower == 3)
            {
                costATK.text = "15";
                costATKvalue = 15;
            }
            else if (playerATK.attackPower == 4)
            {
                costATK.text = "15";
                costATKvalue = 15;
            }
            else if (playerATK.attackPower == 5)
            {
                costATK.text = "18";
                costATKvalue = 18;
            }
            else
            {
                costATK.text = "MAX";
                costATKvalue = 999;
            }


            if(playerHealth.startingHp == 6) {
                costHP.text = "10";
                costHPvalue = 10;
            }
            else if( playerHealth.startingHp == 7) {
                costHP.text = "10";
                costHPvalue = 10;
            }
            else if (playerHealth.startingHp == 8)
            {
                costHP.text = "10";
                costHPvalue = 10;
            }
            else if (playerHealth.startingHp == 9)
            {
                costHP.text = "10";
                costHPvalue = 10;
            }
            else
            {
                costHP.text = "MAX";
                costHPvalue = 999;
            }
           



        }
        else
        {
            if (entered && Input.GetKeyDown(KeyCode.E))
            {
               // Debug.Log("wtf");
                shopMenue.SetActive(true);
                PauseMenue.GamePaused = true;
            }
        }

        
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log("yes");
        if(collision.tag == "Player")
        {
            //Debug.Log("lol");
            entered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            entered = false;
        }
    }
    */

    public void back()
    {
        Time.timeScale = 1f;
        shopMenue.SetActive(false);
        PauseMenue.GamePaused = false;
    }

    public void increaseATK()
    {
        if (score.scoreValue >= costATKvalue)
        {
            score.scoreValue = score.scoreValue - costATKvalue;
           // score.score.text = scorevalue.ToString() + "x";
            playerATK.attackPower += 1;
        }
    }

    public void increaseHP()
    {
        if (score.scoreValue >= costHPvalue)
        {
            score.scoreValue = score.scoreValue - costHPvalue;
            // score.score.text = scorevalue.ToString() + "x";
            playerHealth.startingHp += 1;
            playerHealth.currentHp = playerHealth.startingHp;
        }
    }

}

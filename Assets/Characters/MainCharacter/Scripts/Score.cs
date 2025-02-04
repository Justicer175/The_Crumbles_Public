using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{

    public int scoreValue;
    [SerializeField] public TextMeshProUGUI score;
    // Start is called before the first frame update
    void Start()
    {
        scoreValue = PlayerPrefs.GetInt("score");
        //scoreValue = 97;
        //Debug.Log(scoreValue);
        //Debug.Log(PlayerPrefs.GetInt("score"));
        /*
        Debug.Log(PlayerPrefs.GetInt("score"));
        Debug.Log(PlayerPrefs.GetInt("attack"));
        Debug.Log(PlayerPrefs.GetInt("health"));
        Debug.Log(PlayerPrefs.GetInt("fullHealth"));
        */
    }

    // Update is called once per frame
    void Update()
    {
        score.text = scoreValue.ToString() + "x";
    }
}

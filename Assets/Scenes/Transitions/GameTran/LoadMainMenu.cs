using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
    // [SerializeField] private GameObject canvas;
    [SerializeField] PauseMenue pauseMenue;

    public void Load()
    {
        
        pauseMenue.Resume();
        SceneManager.LoadScene("MainMenue");
    }
}

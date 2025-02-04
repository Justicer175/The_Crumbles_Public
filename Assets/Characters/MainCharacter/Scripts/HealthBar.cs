using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealth;
    [SerializeField] private Image currentHealth;

    private void Start()
    {
        
    }

    private void Update()
    {
        totalHealth.fillAmount = playerHealth.startingHp / 10;
        currentHealth.fillAmount = playerHealth.currentHp / 10;
    }
}

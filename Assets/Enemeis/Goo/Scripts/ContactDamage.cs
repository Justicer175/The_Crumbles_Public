using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] Health playerHealth;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damage);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float PlayerMaxHealth;
    [SerializeField] float PlayerHealth;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI deathScreen;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + PlayerHealth.ToString();
    }

    public void TakeDamage(float damage) {
        PlayerHealth -= damage;
        if( PlayerHealth <= 0) {
            Die();
        }
    }

    private void Die()
    {
        deathScreen.text = "You died";
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] float PlayerMaxHealth;
    [SerializeField] float PlayerHealth;
    [SerializeField] GameObject healthBar;
    [SerializeField] TextMeshProUGUI deathScreen;
    Slider healthBarSlider;
    // Start is called before the first frame update
    void Start()
    {
        healthBarSlider = healthBar.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBarSlider.value = PlayerHealth;
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float PlayerMaxHealth;
    [SerializeField] float PlayerHealth;
    [SerializeField] TextMeshProUGUI healthText;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + PlayerHealth.ToString();
    }
}

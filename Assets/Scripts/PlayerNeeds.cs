using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNeeds : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hungerText;
    [SerializeField] TextMeshProUGUI thirstText;
    [SerializeField] TextMeshProUGUI energyText;
    Core core;
    float energyLeft = 100f;
    float thirst = 0f;
    float hunger = 0f;

    
private void Start() {
    core = GameObject.Find("Core").GetComponent<Core>();
    
}

private void Update() {
    UpdateHungerText();
    UpdateThirstText();
    UpdateEnergyText();
}

    private void UpdateEnergyText()
    {
        energyLeft = core.dayLenght - core.timer;
        energyText.text = "Energy: " + energyLeft;
    }

    private void UpdateThirstText()
    {
        thirst = core.timer;
        thirstText.text = "Thirst: " + thirst;
    }

    private void UpdateHungerText()
    {
        hunger = core.timer;
        hungerText.text = "Hunger: " + hunger;
    }
}

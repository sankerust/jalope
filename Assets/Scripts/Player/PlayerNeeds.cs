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
    [SerializeField] TextMeshProUGUI coldText;
    
    [SerializeField] float maxHunger = 100f;
    [SerializeField] float maxThirst = 100f;
    [SerializeField] float maxCold = -60f;
    [SerializeField] float minEnergy = 0f;
    [SerializeField] float needsDamage = 5f;
    Core core;
    Health health;
    bool canDamage = true;

    public float energyLeft = 100f;
    public float thirst = 0f;
    public float hunger = 0f;
    public float currentCold = 10f;

    
private void Start() {
    core = GameObject.Find("Core").GetComponent<Core>();
    health = GetComponent<Health>();
    
}

private void Update() {

    UpdateNeeds();
}

    private void UpdateNeeds()
    {
        UpdateHungerText();
        UpdateThirstText();
        UpdateEnergyText();
        UpdateColdText();
    }

    private void UpdateColdText()
    {
        coldText.text = "temperature: " + currentCold;
        if (currentCold <= maxCold && canDamage) {
            StartCoroutine(DamageByNeeds());
        }
    }

    private void UpdateEnergyText()
    {
        energyLeft = core.dayLenght - core.timer;
        energyText.text = "Energy: " + energyLeft;
        if (energyLeft <= minEnergy && canDamage) {
            StartCoroutine(DamageByNeeds());
        }
    }

    private void UpdateThirstText()
    {
        thirst = core.timer;
        thirstText.text = "Thirst: " + thirst;
        if (thirst >= maxThirst && canDamage) {
            StartCoroutine(DamageByNeeds());
        }
    }

    private void UpdateHungerText()
    {
        hunger = core.timer;
        hungerText.text = "Hunger: " + hunger;
        if (hunger >= maxHunger && canDamage) {
            StartCoroutine(DamageByNeeds());
        }
    }

    IEnumerator DamageByNeeds() {
        canDamage = false;
        health.TakeDamage(needsDamage);
        yield return new WaitForSeconds(5f);
        canDamage = true;
    }
}

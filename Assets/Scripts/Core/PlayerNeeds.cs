using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerNeeds : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hungerText;
    [SerializeField] TextMeshProUGUI thirstText;
    [SerializeField] TextMeshProUGUI energyText;
    [SerializeField] TextMeshProUGUI coldText;
    [SerializeField] TextMeshProUGUI dyingReasonText;
    
    [SerializeField] float maxHunger = 100f;
    [SerializeField] float maxThirst = 100f;
    [SerializeField] float maxCold = -60f;
    [SerializeField] float minEnergy = 0f;
    [SerializeField] float needsDamage = 5f;

    [SerializeField] AudioClip hungerSound, thirstSound, coldSound, energySound, damageTakenSound;
    Core core;
    Health health;
    AudioSource audioSource;
    bool canDamage = true;
    private float timer;

    public float energyLeft = 100f;
    public float thirst = 0f;
    public float hunger = 0f;
    public float currentTemperature = 10f;
    private float energyTimer;

    
private void Start() {
    core = GameObject.Find("Core").GetComponent<Core>();
    audioSource = GetComponent<AudioSource>();
    health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    dyingReasonText.text = null;
    
}

private void Update() {

    UpdateNeeds();
}

    private void UpdateNeeds()
    {
        UpdateTemperature();
        UpdateHunger();
        UpdateThirst();
        UpdateEnergy();
        
    }

    private void UpdateTemperature()
    {
        coldText.text = "temperature: " + currentTemperature;
        if (currentTemperature <= maxCold && canDamage) {
            StartCoroutine(PlayNeedSound(coldSound));
            StartCoroutine(DamageByNeeds("Dying of cold"));
        }
    }

    private void UpdateEnergy()
    {
        energyTimer += Time.deltaTime;
        energyLeft = core.dayLenght - energyTimer;
        energyText.text = "Energy: " + energyLeft;
        if (energyLeft <= minEnergy && canDamage) {
            StartCoroutine(PlayNeedSound(energySound));
            StartCoroutine(DamageByNeeds("Dying of exhaustion"));
        }
    }

    private void UpdateThirst()
    {
        if(thirst <= 0) {
            thirst = 0;
        }
        thirst += Time.deltaTime;
        thirstText.text = "Thirst: " + thirst;
        if (thirst >= maxThirst && canDamage) {
            StartCoroutine(PlayNeedSound(thirstSound));
            StartCoroutine(DamageByNeeds("Dying of thirst"));
        }
    }

    private void UpdateHunger()
    {
        if(hunger <= 0) {
            hunger = 0;
        }
        hunger += Time.deltaTime;
        hungerText.text = "Hunger: " + hunger;
        if (hunger >= maxHunger && canDamage) {
            StartCoroutine(PlayNeedSound(hungerSound));
            StartCoroutine(DamageByNeeds("Dying of hunger"));
        }
    }
    IEnumerator PlayNeedSound(AudioClip sound) {
        audioSource.PlayOneShot(sound);
        yield return new WaitForSeconds(sound.length + 1f);
    }

    IEnumerator DamageByNeeds(string message) {
        canDamage = false;
        health.TakeDamage(needsDamage);
        dyingReasonText.text = message;
        yield return new WaitForSeconds(2f);
        dyingReasonText.text = null;
        yield return new WaitForSeconds(3f);
        canDamage = true;
    }
}

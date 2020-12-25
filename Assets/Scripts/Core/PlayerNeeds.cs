using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerNeeds : MonoBehaviour
{
    [SerializeField] GameObject energyBar, thirstBar, hungerBar, tempBar;
    Slider energyBarSlider, thirstBarSlider, hungerBarSlider, tempBarSlider;
    [SerializeField] Gradient tempGradient;
    Image tempFill;
    [Space(10)]

    [SerializeField] GameObject messageWindow;
    Text dyingReasonText;

    
    [SerializeField] float maxHunger = 100f, maxThirst = 100f, maxCold = -60f, minEnergy = 0f, needsDamage = 5f;
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
    dyingReasonText = messageWindow.GetComponentInChildren<Text>();
    dyingReasonText.text = null;

    energyBarSlider = energyBar.GetComponent<Slider>();
    thirstBarSlider = thirstBar.GetComponent<Slider>();
    hungerBarSlider = hungerBar.GetComponent<Slider>();
    tempBarSlider = tempBar.GetComponent<Slider>();
    tempFill = tempBar.GetComponentInChildren<Image>();
    
    energyBarSlider.maxValue = core.dayLenght;
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
        tempFill.color = tempGradient.Evaluate(tempBarSlider.normalizedValue);
        tempBarSlider.value = currentTemperature;
        //coldText.text = "temperature: " + currentTemperature;
        if (currentTemperature <= maxCold && canDamage) {
            StartCoroutine(PlayNeedSound(coldSound));
            StartCoroutine(DamageByNeeds("Dying of cold"));
        }
    }

    private void UpdateEnergy()
    {
        energyTimer += Time.deltaTime;
        energyLeft = core.dayLenght - energyTimer;
        energyBarSlider.value = energyLeft;
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
        thirstBarSlider.value = maxThirst - thirst;
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
        hungerBarSlider.value = maxHunger - hunger;
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
        messageWindow.SetActive(true);
        dyingReasonText.text = message;
        yield return new WaitForSeconds(2f);
        dyingReasonText.text = null;
        messageWindow.SetActive(false);
        yield return new WaitForSeconds(3f);
        canDamage = true;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdFront : MonoBehaviour
{
    PlayerNeeds playerNeeds;
    GameObject player;
    GameObject core;
    [SerializeField] float frontSpeed = 1f;
    [SerializeField] float baseTemperature = 0f;
    [SerializeField] float maxCold = -200f;
    float distanceToPlayer;
    float updatedTemperature;
    Vector3 playerPosition;

    private void Awake() {

    }
    void Start()
    {
        core = GameObject.Find("Core");
        playerNeeds = core.GetComponent<PlayerNeeds>();
        player = GameObject.FindGameObjectWithTag("Player");
        distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);
    }

    void Update()
    {
        GetDistanceToPlayer();
        ChasePlayer();
        ApplyCold();
    }

    private void GetDistanceToPlayer()
    {
        playerPosition = player.transform.position;
        distanceToPlayer = Mathf.Clamp(Vector3.Distance(this.transform.position, playerPosition),10, Mathf.Infinity);
    }

    private void ChasePlayer()
    {
        this.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, frontSpeed);
    }

    private void ApplyCold() {
        updatedTemperature = (100 - 1000 / Mathf.Pow(Mathf.Log10(distanceToPlayer), 2));
        if (updatedTemperature > maxCold && updatedTemperature < baseTemperature) {
            playerNeeds.currentTemperature = updatedTemperature;
        } else if (updatedTemperature < maxCold) {
            playerNeeds.currentTemperature = maxCold;
            } 
    }
}

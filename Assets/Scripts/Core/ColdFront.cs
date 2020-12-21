using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdFront : MonoBehaviour
{
    PlayerNeeds playerNeeds;
    GameObject player;
    GameObject core;
    [SerializeField] float frontSpeed = 1f;
    float distanceToPlayer;

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
        distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        this.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, frontSpeed);
        playerNeeds.currentTemperature = -10000/distanceToPlayer;
        print(player.transform.position);
    }
}

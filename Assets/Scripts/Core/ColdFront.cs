using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdFront : MonoBehaviour
{
    GameObject player;
    [SerializeField] float frontSpeed = 1f;
    [SerializeField] float firstRange = 400f;
    [SerializeField] float coldStep = 10f;
    float distanceToPlayer;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        this.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, frontSpeed);

        //print(distanceToPlayer);
        //ApplyCold();
    }

    /*private void ApplyCold() {
        if(distanceToPlayer <= firstRange && distanceToPlayer >= secondRange)
        player.GetComponent<PlayerNeeds>().currentCold -= (distanceToPlayer/10000);
}*/}

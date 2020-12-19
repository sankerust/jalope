using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkPickup : MonoBehaviour, IUsable
{
    GameObject player;
    [SerializeField] float amountRestored = 25f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Use() {
        player.GetComponent<PlayerNeeds>().thirst += amountRestored;
        print("i drank");
    }
}

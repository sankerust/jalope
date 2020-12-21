using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPickup : MonoBehaviour, IUsable
{
    GameObject core;
    [SerializeField] float amountRestored = 25f;

    void Start()
    {
        core = GameObject.Find("Core");
    }

    public void Use() {
        core.GetComponent<PlayerNeeds>().hunger -= amountRestored;
        print("i ate");
        Destroy(gameObject);
    }
}

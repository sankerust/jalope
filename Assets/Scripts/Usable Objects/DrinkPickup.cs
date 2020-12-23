using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkPickup : MonoBehaviour, IUsable
{
    GameObject core;
    AudioSource audioSource;
    [SerializeField] float amountRestored = 25f;

    void Start()
    {
        core = GameObject.Find("Core");
    }

    public void Use() {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        GetComponent<MeshRenderer>().enabled = false;
        core.GetComponent<PlayerNeeds>().thirst -= amountRestored;
        
        Destroy(gameObject, audioSource.clip.length);
    }
}

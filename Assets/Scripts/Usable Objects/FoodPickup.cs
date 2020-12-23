using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FoodPickup : MonoBehaviour, IUsable
{

    GameObject core;
    AudioSource audiosource;
    [SerializeField] float amountRestored = 25f;

    void Start()
    {
        core = GameObject.Find("Core");
    }

    public void Use() {
        audiosource = GetComponent<AudioSource>();
        audiosource.Play();
        GetComponent<MeshRenderer>().enabled = false;
        core.GetComponent<PlayerNeeds>().hunger -= amountRestored;
        print("i ate");
        Destroy(gameObject,audiosource.clip.length);
    }
}

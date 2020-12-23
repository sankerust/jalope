using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FuelCan : MonoBehaviour, IUsable
{
    GameObject car;
    [SerializeField] float amountRestored = 25f;
    [SerializeField] float rangeOfUse = 2f;
    [SerializeField] AudioClip refuelSound;
    [SerializeField] AudioClip isEmptySound;
    AudioSource audioSource;
    bool isEmpty = false;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    public void Use() {
        car = GameObject.FindGameObjectWithTag("Car");
        if (!isEmpty) {
        float distanceToCar = Vector3.Distance(gameObject.transform.position, car.transform.position);
        if (distanceToCar > rangeOfUse) {
            print("vehicle too far");
            return;
        }

        car.GetComponent<CarCondition>().fuelLeft += amountRestored;
        print("filled up da car");
        gameObject.GetComponent<Rigidbody>().mass = 1;
        isEmpty = true;
        audioSource.PlayOneShot(refuelSound);
        return;
        }

        if(isEmpty) {
            audioSource.PlayOneShot(isEmptySound);
            print("The can is empty");
        }

    }
}

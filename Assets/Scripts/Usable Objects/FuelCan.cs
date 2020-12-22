using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCan : MonoBehaviour, IUsable
{
    GameObject car;
    [SerializeField] float amountRestored = 25f;
    [SerializeField] float rangeOfUse = 2f;
    bool isEmpty = false;
    // Start is called before the first frame update
    void Start()
    {
        car = GameObject.FindGameObjectWithTag("Car");
    }
    public void Use() {
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
        return;
        }

        if(isEmpty) {
            print("The can is empty");
        }

    }
}

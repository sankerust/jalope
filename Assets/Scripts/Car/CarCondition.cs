using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarCondition : MonoBehaviour
{
    public float fuelLeft = 100f;
    public TextMeshProUGUI fuelText;
    CarController carController;
    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fuelLeft <= 0) {
            fuelLeft = 0;
        }
        fuelText.text = "Fuel left: " + fuelLeft; 
    }
}

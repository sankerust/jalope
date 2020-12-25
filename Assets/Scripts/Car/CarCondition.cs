using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarCondition : MonoBehaviour
{
    [SerializeField] float maxFuel = 100f;
    public float fuelLeft = 100f;
    public GameObject fuelBar;
    Slider fuelBarSlider;
    CarController carController;
    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponent<CarController>();
        fuelBarSlider = fuelBar.GetComponent<Slider>();
        fuelBarSlider.maxValue = maxFuel;
    }

    // Update is called once per frame
    void Update()
    {
        fuelBarSlider.value = fuelLeft;
        if (fuelLeft <= 0) {
            fuelLeft = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarButton : MonoBehaviour, IUsable
{

    void Start()
    {

    }

    public void Use() {
        print("used button in car");
    }
}

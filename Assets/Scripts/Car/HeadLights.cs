using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLights : MonoBehaviour
{
    bool isOn = false;
    void Update()
    {
        if(Input.GetKeyDown("l")) {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(!isOn);
            };
            isOn = !isOn;
        }
    }
}

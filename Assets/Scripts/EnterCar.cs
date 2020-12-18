using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCar : MonoBehaviour
{
    GameObject player;
    CarController carController;
    private void Start() {
        player = GameObject.FindWithTag("Player");
        carController = GameObject.FindWithTag("Car").GetComponent<CarController>();
        print(carController);
    }
    private void OnTriggerStay(Collider who) {
        CharacterController characterController = player.GetComponent<CharacterController>();
        if (who == characterController && Input.GetKeyDown("e")) {
            player.SetActive(false);
            
            carController.enabled = true;

        }
    }
}

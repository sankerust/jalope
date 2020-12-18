using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCar : MonoBehaviour
{
    GameObject player;
    CarController carController;
    [SerializeField] Camera CarCam;
    [SerializeField] Transform exitPositionObject;
    Vector3 exitPosition;
    public bool isInCar = false;
    bool canEnter = false;

    private void Start() {
        player = GameObject.FindWithTag("Player");
        carController = GameObject.FindWithTag("Car").GetComponent<CarController>();
        exitPosition = exitPositionObject.transform.position;
    }

    private void Update() {
        UpdateExitPosition();
        EnterVehicle();
        ExitCar();
    }

    private void UpdateExitPosition() {
        exitPosition = exitPositionObject.transform.position;
    }
    private void OnTriggerEnter(Collider other) {
        CharacterController characterController = player.GetComponent<CharacterController>();
        if( other == characterController) {
            canEnter = true;
        }
        
    }
    private void OnTriggerExit(Collider other) {
        CharacterController characterController = player.GetComponent<CharacterController>();
        if( other == characterController) {
            canEnter = false;
        }
    }

    private void EnterVehicle() {
        if(canEnter && Input.GetKeyDown("e") && !isInCar) {
            player.SetActive(false);
            carController.enabled = true;
            CarCam.gameObject.SetActive(true);
            isInCar = true;
        }
    }

    private void ExitCar() {

        if (Input.GetKeyDown("f") && isInCar) {
            player.transform.position = exitPosition;
            CarCam.gameObject.SetActive(false);
            isInCar = false;
            carController.enabled = false;
            player.SetActive(true);
            canEnter = false;
        }
    }
}

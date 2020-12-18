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

    private void Start() {
        player = GameObject.FindWithTag("Player");
        carController = GameObject.FindWithTag("Car").GetComponent<CarController>();
        exitPosition = exitPositionObject.transform.position;
    }

    private void Update() {
        UpdateExitPosition();
        ExitCar();
    }

    private void UpdateExitPosition() {
        exitPosition = exitPositionObject.transform.position;
    }

    private void OnTriggerStay(Collider who) {
        CharacterController characterController = player.GetComponent<CharacterController>();
        if (Input.GetKeyDown("e") && !isInCar && who == characterController) {
            player.SetActive(false);
            carController.enabled = true;
            CarCam.gameObject.SetActive(true);
            isInCar = true;
            print("ontrigger car called");
        }
    }

    private void ExitCar() {

        if (Input.GetKeyDown("f") && isInCar) {
            CarCam.gameObject.SetActive(false);
            isInCar = false;
            carController.enabled = false;
            player.SetActive(true);
            player.transform.position = exitPosition;
            print("exit car called");
        }
    }
}

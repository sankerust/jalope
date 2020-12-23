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
    public bool canEnter = false;
    AudioSource audioSource;
    [SerializeField] AudioClip doorOpenSound;
    [SerializeField] AudioClip doorCloseSound;
    private void Awake() {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start() {
        player = GameObject.FindWithTag("Player");
        carController = GameObject.FindWithTag("Car").GetComponent<CarController>();
        exitPosition = exitPositionObject.transform.position;
    }

    private void Update() {
        UpdateExitPosition();
        StartCoroutine(EnterVehicle());
        StartCoroutine(ExitCar());
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

    IEnumerator EnterVehicle() {
        if(canEnter && Input.GetKeyDown("e") && !isInCar) {
            audioSource.clip = doorOpenSound;
            audioSource.Play();
            player.GetComponent<PlayerMovement>().enabled = false;
            yield return new WaitForSeconds(audioSource.clip.length);

            player.GetComponent<PlayerMovement>().enabled = true;
            player.SetActive(false);
            player.gameObject.transform.SetParent(carController.transform);
            
            carController.enabled = true;
            CarCam.gameObject.SetActive(true);
            isInCar = true;
            audioSource.clip = doorCloseSound;
            audioSource.Play();
        }
    }

    IEnumerator ExitCar() {

        if (Input.GetKeyDown("f") && isInCar && !carController.VehicleIsMoving()) {
            audioSource.clip = doorOpenSound;
            audioSource.Play();

            yield return new WaitForSeconds(audioSource.clip.length);

            audioSource.clip = doorCloseSound;
            audioSource.Play();

            player.transform.position = exitPosition;
            player.gameObject.transform.SetParent(null);
            player.SetActive(true);
            CarCam.gameObject.SetActive(false);
            isInCar = false;
            carController.enabled = false;
            //SwitchOffPlayer(false);
            canEnter = false;
        }
    }
}

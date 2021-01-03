using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] float cameraFov = 80f;
    [SerializeField] float zoomFov = 30f;
    [SerializeField] Transform playerBody;
    float xRotation = 0f;
    float yRotation = 0f;
    bool isZoomed = false;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Camera camera = Camera.main;
    }
    private void Update()
    {
        Zoom();
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        if (playerBody != null) {
            playerBody.Rotate(Vector3.up * mouseX);
        } else {
            yRotation += mouseX;
            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }
    }

    private void Zoom() {
        if (Input.GetButton("Fire2") && !isZoomed) {
            isZoomed = true;
            Camera.main.fieldOfView = zoomFov;
            return;
        }

        if (Input.GetButton("Fire2") && isZoomed) {
            isZoomed = false;
            Camera.main.fieldOfView = cameraFov;
            return;
        }
        
    }
}

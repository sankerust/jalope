using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;
    float steerAngle;

    [SerializeField] WheelCollider frontDriverWheel, frontPassengerWheel;
    [SerializeField] WheelCollider rearDriverWheel, rearPassengerWheel;
    public Transform frontDriverTransform, frontPassengerTransform;
    public Transform rearDriverTransform, rearPassengerTransform;

    [SerializeField] float maxSteerAngle = 30f;
    [SerializeField] float motorForce = 50f;
    [SerializeField] float brakePower = 300f;

    private void FixedUpdate() {
        GetInput();
        Steer();
        Accelerate();
        Brake();
        UpdateWheelPoses();
        ExitCar();
    }
    public void GetInput() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer() {
        steerAngle = maxSteerAngle * horizontalInput;
        frontDriverWheel.steerAngle = steerAngle;
        frontPassengerWheel.steerAngle = steerAngle;
    }

    private void Accelerate() {
        frontDriverWheel.motorTorque = verticalInput * motorForce;
        frontPassengerWheel.motorTorque = verticalInput * motorForce;
    }
        private void Brake()
    {
        if (Input.GetKey("space")) {
            print("called");
            frontDriverWheel.brakeTorque = brakePower;
            frontPassengerWheel.brakeTorque = brakePower;
            return;
        }
        frontDriverWheel.brakeTorque = 0f;
        frontPassengerWheel.brakeTorque = 0f;
    }
    private void UpdateWheelPoses() {
        UpdateWheelPose(frontDriverWheel, frontDriverTransform);
        UpdateWheelPose(frontPassengerWheel, frontPassengerTransform);
        UpdateWheelPose(rearDriverWheel, rearDriverTransform);
        UpdateWheelPose(rearPassengerWheel, rearPassengerTransform);
    }

    private void UpdateWheelPose(WheelCollider collider, Transform transform) {
        Vector3 position = transform.position;
        Quaternion quaternion = transform.rotation;

        collider.GetWorldPose(out position, out quaternion);

        transform.position = position;
        transform.rotation = quaternion;
    }
    
    private void ExitCar() {
        if (Input.GetKeyDown("e")) {
            this.enabled = false;
        }
    }

    private void OnDisable() {
        horizontalInput = 0;
        verticalInput = 0;
    }



}

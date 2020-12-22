using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;
    float steerAngle;
    Vector3 carVelocity;

    [SerializeField] WheelCollider frontDriverWheel, frontPassengerWheel;
    [SerializeField] WheelCollider rearDriverWheel, rearPassengerWheel;
    public Transform frontDriverTransform, frontPassengerTransform;
    public Transform rearDriverTransform, rearPassengerTransform;
    private List<WheelCollider> frontAxle;
    private List<WheelCollider> rearAxle;

    private List<WheelCollider> allWheels;
    CarCondition carCondition;
    Rigidbody rigidbody;

    [SerializeField] float maxSteerAngle = 30f;
    [SerializeField] float enginePower = 1000f;
    float appliedMotorForce;
    [SerializeField] float brakePower = 300f;

    private void Awake() {
        carCondition = GetComponent<CarCondition>();
        rigidbody = GetComponent<Rigidbody>();

        frontAxle = new List<WheelCollider>();
        frontAxle.Add(frontDriverWheel);
        frontAxle.Add(frontPassengerWheel);

        rearAxle = new List<WheelCollider>();
        rearAxle.Add(rearDriverWheel);
        rearAxle.Add(rearPassengerWheel);

        allWheels = new List<WheelCollider>();
        allWheels.Add(frontDriverWheel);
        allWheels.Add(frontPassengerWheel);
        allWheels.Add(rearDriverWheel);
        allWheels.Add(rearPassengerWheel);

    }
    


    private void FixedUpdate() {
        GetInput();
        Steer();
        Accelerate();
        Brake();
        UpdateWheelPoses();
        //ExitCar();
    }
    
    public void GetInput() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private bool CarMovingForward() {
        carVelocity = rigidbody.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(rigidbody.velocity);
        return localVelocity.z > 0;
    }

    public bool VehicleIsMoving() {
        if (rigidbody.velocity.magnitude < 0.2) {
            return false;
        } else {
            return true;
        }
        
    }

    private void Steer() {
        steerAngle = maxSteerAngle * horizontalInput;
        frontDriverWheel.steerAngle = steerAngle;
        frontPassengerWheel.steerAngle = steerAngle;
    }

    private void Accelerate() {
        CheckFuel();
        
        carCondition.fuelLeft = carCondition.fuelLeft - (Mathf.Abs(verticalInput) * 0.1f);
        foreach(WheelCollider wheel in frontAxle) {
            wheel.motorTorque = verticalInput * appliedMotorForce;
            } 
    }

    private void CheckFuel() {
        if (carCondition.fuelLeft == 0) {
         appliedMotorForce = 0f;   
        } else {
            appliedMotorForce = enginePower;
        }
    }
    private void Brake() {
        if (Input.GetKey(KeyCode.Space)) {
            print("braking");
            foreach(WheelCollider wheel in allWheels) {
                wheel.brakeTorque = brakePower;
            }
            return;
        }
        foreach(WheelCollider wheel in allWheels) {
                wheel.brakeTorque = 0f;
            }
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
        if (Input.GetKeyDown("e") && !VehicleIsMoving()) {
            this.enabled = false;
        }
    }

    private void OnDisable() {
        horizontalInput = 0;
        verticalInput = 0;
        foreach(WheelCollider wheel in allWheels) {
                wheel.brakeTorque = brakePower;
            }
        carCondition.fuelText.enabled = false;
    }

    private void OnEnable() {
        carCondition.fuelText.enabled = true;
    }



}

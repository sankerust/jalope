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
    CarCondition carCondition;
    Rigidbody rigidbody;
    AudioSource audioSource;
    private List<WheelCollider> frontAxle;
    private List<WheelCollider> rearAxle;

    private List<WheelCollider> allWheels;

    [SerializeField] WheelCollider frontDriverWheel, frontPassengerWheel;
    [SerializeField] WheelCollider rearDriverWheel, rearPassengerWheel;
    public Transform frontDriverTransform, frontPassengerTransform;
    public Transform rearDriverTransform, rearPassengerTransform;
    [Space(10)]
    [SerializeField] float maxSteerAngle = 30f;
    [SerializeField] float enginePower = 1000f;
    [SerializeField] float brakePower = 300f;
    float appliedMotorForce;
    float wheelRadius, wheelRpm, circumFerence, normalizedSpeed, speedOnKmh;
    private bool engineRunning = false;
    
    [Space(10)]
    [SerializeField] AudioClip starterSound;
    [SerializeField] AudioClip engineSound;
    [SerializeField] AudioClip engineStopSound;

    [Space(10)]
    [SerializeField] float lerpStepForSound = 0.1f;
    [SerializeField] float firstGearMax = 30;
    [SerializeField] float secondGearMax = 60;
    [SerializeField] float thirdGearMax = 90;
    [SerializeField] float fourthGearMax = 120;
    [Space(10)]
    
    public float engineSoundPitch = 0.3f;
    public int carSpeed;
    private float minSpeed, maxSpeed;


    private void Awake() {
        carCondition = GetComponent<CarCondition>();
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

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
        GetAxisInput();
        Steer();
        Brake();
        UpdateWheelPoses();
        Accelerate();
    }

    private void GetAxisInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void Update() {
        GetInput();
        CheckFuel();
        speedometerVelocity();
        PlayEngineSound();
    }

 private void speedometerRPM(){
     // incorrect
         wheelRadius = frontDriverWheel.radius; 
         wheelRpm = (frontDriverWheel.rpm + frontPassengerWheel.rpm) / 2f; 

         circumFerence = 2.0f * 3.14f * wheelRadius; // Finding circumFerence 2 Pi R
         speedOnKmh = circumFerence * wheelRpm *60; 
         normalizedSpeed = (speedOnKmh - 0) / (140 - 0);
     }
     private void speedometerVelocity() {
         carSpeed = Mathf.RoundToInt(rigidbody.velocity.magnitude * 3.6f);
     }

     private void PlayEngineSound() {
         if (engineRunning) {
            float idlingModifier = 1f;

            if(carSpeed < firstGearMax) {
                minSpeed = 0f;
                maxSpeed = firstGearMax;
            }

            if(carSpeed > firstGearMax && carSpeed < secondGearMax) {
                minSpeed = firstGearMax;
                maxSpeed = secondGearMax;
            }

            if(carSpeed > secondGearMax && carSpeed < thirdGearMax) {
                minSpeed = secondGearMax;
                maxSpeed = thirdGearMax;
            }

            if(carSpeed > thirdGearMax && carSpeed < fourthGearMax) {
                minSpeed = thirdGearMax;
                maxSpeed = fourthGearMax;
            }
            if(VehicleIsMoving()) {
                idlingModifier = 2f;
            }

            normalizedSpeed = (carSpeed - minSpeed) / (maxSpeed - minSpeed);
            audioSource.pitch = Mathf.Lerp(audioSource.pitch, (normalizedSpeed + engineSoundPitch * idlingModifier) , lerpStepForSound);
         } else {
             return;
         }
         
     }
    public void GetInput() {
        if(Input.GetKeyDown("e") && !engineRunning) {
            StartCoroutine(StartEngine());
        }
        if(Input.GetKeyDown("e") && engineRunning && !VehicleIsMoving()) {
            StartCoroutine(StopEngine());
        }
    }
    IEnumerator StopEngine() {
        engineRunning = false;
        audioSource.Stop();
        audioSource.pitch = 1f;
        audioSource.clip = engineStopSound;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSource.Stop();
    }
    IEnumerator StartEngine() {
        audioSource.clip = starterSound;
        audioSource.Play();
        yield return new WaitForSeconds(1.5f);
        audioSource.Stop();
        audioSource.clip = engineSound;
        engineRunning = true;
        audioSource.Play();
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
        if(engineRunning && carCondition.fuelLeft > 0) {
            carCondition.fuelLeft = carCondition.fuelLeft - (Mathf.Abs(verticalInput) * 0.1f);
            
            foreach(WheelCollider wheel in frontAxle) {
                wheel.motorTorque = verticalInput * enginePower;
                } 
        } else {
            foreach(WheelCollider wheel in frontAxle) {
                wheel.motorTorque = 0f;
                } 
        }

    }

    private void CheckFuel() {
        if (carCondition.fuelLeft == 0f && engineRunning) {
            StartCoroutine(StopEngine());
        }
    }
    private void Brake() {
        if (Input.GetKey(KeyCode.Space)) {
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

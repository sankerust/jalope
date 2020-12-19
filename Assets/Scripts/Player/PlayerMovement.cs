using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float sprintSpeed = 8f;

    [SerializeField] float gravity = -9.81f;

    [SerializeField] Transform groundCheck;

    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] float jumpHeight = 3f;

    [SerializeField] LayerMask groundMask;

    bool isGrounded;

    Vector3 velocity;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        if (isGrounded && Input.GetButtonDown("Jump")) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //if (Input.GetKey(KeyCode.LeftShift)) {
            //moveSpeed = sprintSpeed;
        //}

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //velocity = Vector3.ClampMagnitude(velocity, 1f);
    }
}

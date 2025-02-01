using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class XRMovement : MonoBehaviour
{
    public float speed = 1000.0f; // Movement speed
    private CharacterController characterController;
    public Transform cameraTransform; // Assign the main camera here in the Inspector

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (!characterController)
        {
            UnityEngine.Debug.LogError("CharacterController is missing from the XR Rig!");
        }

        if (!cameraTransform)
        {
            UnityEngine.Debug.LogError("Camera Transform is not assigned!");
        }
        //Debug.Log("Bunaaaa");
    }

    private void Update()
    {
        // Get input from WASD or arrow keys
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float vertical = Input.GetAxis("Vertical"); // W/S or Up/Down

        //Debug.Log($"Horizontal: {horizontal}, Vertical: {vertical}");

        // Get the camera's forward and right direction
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Flatten the directions to ignore vertical movement (Y-axis)
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // Calculate movement direction relative to the camera's current orientation
        Vector3 move = (forward * vertical + right * horizontal) * speed;
        //Debug.Log($"Move vector: {move}");

        // Move the character controller
        characterController.Move(move * Time.deltaTime);
        //Debug.Log("PAAAAAAA");
    }
}

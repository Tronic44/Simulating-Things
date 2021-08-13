using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CameraController))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 5;
    Camera viewCamera;
    CameraController controller;

    void Start()
    {
        controller = GetComponent<CameraController>();
        viewCamera = Camera.main;
    }

    
    void Update()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

    }
}

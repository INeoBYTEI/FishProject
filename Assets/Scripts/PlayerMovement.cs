using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    Camera mainCam;
    private InputAction moveInputs;
    private Rigidbody rb;
    public float speed = 5f;

    private void Start()
    {
        moveInputs = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
        mainCam = Camera.main;
    }
    private void Update()
    {
        Movement();
    }

    void Movement()
    {
        Vector3 move = moveInputs.ReadValue<Vector2>();
        Vector3 camForward = mainCam.transform.forward;
        camForward.y = 0; // Keep movement on the XZ plane
        Vector3 camRight = mainCam.transform.right;
        Vector3 newPos = transform.position + (camForward * move.y + camRight * move.x) * speed * Time.deltaTime;
        rb.MovePosition(newPos);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float speed = 5f;
    float horizontalInput;
    float verticalInput;
    Rigidbody rb;
    float sensitivity = 5f;
    float mouseX;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");

        transform.Rotate(Vector3.up * mouseX * sensitivity);

        Vector3 movementDirection = transform.right * horizontalInput + transform.forward * verticalInput;

        rb.velocity = new Vector3(movementDirection.x * speed, rb.velocity.y, movementDirection.z * speed);
    }
}

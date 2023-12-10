using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;

    float horizontalInput;
    float verticalInput;
    float mouseX;
    float mouseY;
    float currentCameraRotationX = 0f;

    float sensitivity = 3f;
    float maxCameraAngle = 40f;  
    float minCameraAngle = -30f;

    bool isGrounded;

    public Transform cameraTransform;

    Rigidbody rb;

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
        mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(0, mouseX * sensitivity, 0);

        currentCameraRotationX -= mouseY * sensitivity;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, minCameraAngle, maxCameraAngle);
        cameraTransform.localEulerAngles = new Vector3(currentCameraRotationX, transform.eulerAngles.y, 0f);

        Vector3 movementDirection = transform.right * horizontalInput + transform.forward * verticalInput;

        rb.velocity = new Vector3(movementDirection.x * speed, rb.velocity.y, movementDirection.z * speed);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; 
        }

        cameraTransform.position = transform.position;

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }


}

using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float walkSpeed = 5f;
    [SerializeField]
    public float runSpeed = 15f;
    [SerializeField]
    public float jumpForce = 5f;
    [SerializeField]
    float raycastDistance = 3;
    Vector3 movement;

    Rigidbody rb;
    public float maxCameraAngle = 80f;
    float verticalRotation = 0;
    bool devMode = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.P))
        {
            devMode = true;
        }
        if (Input.GetKey(KeyCode.LeftShift) && devMode)
        {
            movement = new Vector3(horizontal, 0, vertical) * runSpeed;
        }
        else
        {
            movement = new Vector3(horizontal, 0, vertical) * walkSpeed;
        }
     

        Vector3 newPosition = rb.position + rb.transform.TransformDirection(movement);

        rb.MovePosition(newPosition);
    }

    public void Update()
    {
        
        //Rozglądanie się na boki
        float horizontalRotation = Input.GetAxis("Mouse X");
        transform.Rotate(0, horizontalRotation, 0);

        //Rozglądanie się góra dół
        verticalRotation -= Input.GetAxis("Mouse Y");
        verticalRotation = Mathf.Clamp(verticalRotation, -maxCameraAngle, maxCameraAngle);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        //Poruszanie graczem
        Jump();

    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            }
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, raycastDistance);
    }
}
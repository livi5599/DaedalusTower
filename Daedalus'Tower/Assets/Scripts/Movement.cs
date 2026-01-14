using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SecurityMovement : MonoBehaviour
{
    public float sensX;
    public float sensY;
    float xRotation;
    float yRotation;
    public Transform cameraTransform;
    public float playerAcceleration;
    public float playerMaxVelocity;
    private Rigidbody playerRigidbody;
    private bool isAccelerating = false;
    public bool canMove = true;
    [SerializeField] int hidingSpeed = 30;

    
    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameraTransform.transform.localRotation = Quaternion.Euler(90, 0, 0);
    }
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Vector3 moveVec = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        cameraTransform.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
        if (Math.Abs(Input.GetAxisRaw("Vertical")) > 0.1f)
        {
            isAccelerating = true;
        }
        else if (Math.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f)
        {
            isAccelerating = true;
        }
        else { isAccelerating = false; }


        
    }

    void FixedUpdate()
    {
        if (isAccelerating && canMove)
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                playerRigidbody.AddForce(playerAcceleration * transform.forward);
            }
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                playerRigidbody.AddForce(playerAcceleration * -transform.forward);
            }
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                playerRigidbody.AddForce(playerAcceleration * transform.right);
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                playerRigidbody.AddForce(playerAcceleration * -transform.right);
            }
            playerRigidbody.linearVelocity = Vector3.ClampMagnitude(playerRigidbody.linearVelocity, playerMaxVelocity);
        }
    }
    
    public void HidingMovement(Transform hidingSpot)
    {
        while(transform.position != hidingSpot.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, hidingSpot.position, hidingSpeed * Time.deltaTime);   
        }
    }
}

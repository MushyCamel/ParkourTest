using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    public Transform orientation;
    public PlayerMovement playerMovement;
    private Vector3 moveDirection;
    [SerializeField]
    private float moveDownTime = 2;
    [SerializeField]
    private float RampUpTime = 3;


    // Update is called once per frame
    void FixedUpdate()
    {

        //Vector3 forward = transform.TransformDirection(Vector3.forward);
        //Vector3 dRight = transform.TransformVector(1, 0, 1);
        //Vector3 dLeft = transform.TransformVector(-1, 0, 1);

        WallRunRight();
        WallRunLeft();
    }

     void WallRunLeft()
    {
        Vector3 left = orientation.transform.TransformDirection(-Vector3.right);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, left, out hit, 5))
        {
            if (hit.collider != null)
            {
                Debug.DrawRay(transform.position, left, Color.red, 5);
                if (Vector3.Dot(hit.normal, Vector3.up) <= Mathf.Abs(1))
                {
                    Debug.Log("I can wallrun to my Left");
                    WallRun(hit);
                }
            }
        }
    }

    void WallRun(RaycastHit hit)
    {
       var wallRunDirection = Vector3.Cross(hit.normal, Vector3.up);
        moveDirection = wallRunDirection;
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection.Normalize();
        moveDirection *= playerMovement.moveSpeed + (playerMovement.maxSpeed * (moveDownTime / RampUpTime));
    }

    protected void WallRunRight()
    {
        Vector3 right = orientation.transform.TransformDirection(Vector3.right);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, right, out hit, 5))
        {
            Debug.Log("something is to my right");
            if (hit.collider != null)
            {
                Debug.DrawRay(transform.position, right, Color.red, 5);
                Debug.Log("something is to my right and has a collider");
                if (Vector3.Dot(hit.normal, Vector3.up) == 0)
                {
                    Debug.Log("I can wallrun to my right");
                    WallRun(hit);
                }
            }

        }
    }
}

﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    public Transform orientation;
    public PlayerMovement playerMovement;
    private Vector3 moveDirection;
    public Rigidbody rb;


    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector3 forward = transform.TransformDirection(Vector3.forward);
        //Vector3 dRight = transform.TransformVector(1, 0, 1);
        //Vector3 dLeft = transform.TransformVector(-1, 0, 1);

        WallRunCheck();
    }

     void WallRunCheck()
    {
        for(float i = 0; i < Mathf.PI; i += .5f)
        {
            var vec = new Vector3(Mathf.Cos(i), 0, Mathf.Sin(i));
            vec.Normalize();
            vec = playerMovement.orientation.TransformDirection(vec);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, vec, out hit, 5))
            {
                if (hit.collider != null && hit.collider.tag == "WallRunable")
                {
                    //Debug.DrawRay(transform.position, vec, Color.red, 5);
                    if (Vector3.Dot(hit.normal, Vector3.up) <= Mathf.Abs(1))
                    {
                        Debug.Log("I can wallrun to my Left");
                        WallRun(hit);
                        break;
                    }
                }
            }
        }
    }


    void WallRun(RaycastHit hit)
    {
        var wallRunDirection = Vector3.Cross(hit.normal, Vector3.up);
        if (Mathf.Abs(Vector3.Dot(playerMovement.orientation.forward, wallRunDirection)) > 0.5f)
        {
            moveDirection = wallRunDirection;
            Debug.DrawRay(hit.point, wallRunDirection, Color.red, 5);
        }
        else
        {
            moveDirection = -wallRunDirection;
            Debug.DrawRay(hit.point, wallRunDirection, Color.blue, 5);

        }
        moveDirection.Normalize();
        this.rb.useGravity = false;
        transform.position += moveDirection * 10 * Time.fixedDeltaTime;
        this.rb.useGravity = true;

    }
}

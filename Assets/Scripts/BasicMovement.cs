using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BasicMovement : MonoBehaviour
{
    public GameObject parent;
    public bool jumping;

    public float FrictionK = 2.0f;
    public const float gravityK = -9.8f;

    public const float playerMassK = 70.0f;
    public const float playerAccK = 300.0f;

    public float inputFx = 0.0f;
    public float inputFy = 0.0f;
    public float groundFriction;
    public float Fx;
    public float Fy;
    public float Ax = 0.0f;
    public float Ay = 0.0f;
    public float Vx = 0.0f;
    public int Dir = 1;
    public float Vy = 0.0f;
    public Vector3 disp;
    float calcFrictSlow;

    void Update()
    {
        if (transform.position.y <= 0)
        {
            Vy = 0.0f;
        }
        if (transform.position.y > 0.02)
        {
            jumping = true;
        }
        else
        {
            jumping = false;
        }

        if (!jumping & Input.GetKey("space"))
        {
            inputFy = playerAccK * 50;
        }
        else
        {
            inputFy = 0.0f;
        }

        inputFx = (Input.GetKey("a") & !Input.GetKey("d") ? -1.0f :
                 !Input.GetKey("a") & Input.GetKey("d") ? +1.0f : 0.0f) * playerAccK;

        float oldVx = Vx;
        float newVx = Vx;
        if (jumping)
        {
            inputFx = 0;
            groundFriction = 0;
        }
        else
        {
            groundFriction = FrictionK * playerMassK * -Dir;
        }

        Ax = inputFx / playerMassK;
        newVx += 0.5f * Ax * Time.deltaTime * 30;
        calcFrictSlow = 0.5f * (groundFriction / playerMassK) * Time.deltaTime * 30;
        if (Math.Abs(calcFrictSlow) > Math.Abs(newVx))
        {
            newVx = (oldVx/2);
        }
        else
        {
            newVx += calcFrictSlow;
        }

        if (newVx > 0.0f)
        {
            Dir = 1;
        }
        if (newVx < 0.0f)
        {
            Dir = -1;
        }
        if (newVx == 0)
        {
            Dir = 0;
        }
        Debug.Log(newVx);

        Fy = inputFy + gravityK * playerMassK;
        Ay = Fy / playerMassK;

        if (transform.position.y >= 0)
        {
            Vy += 0.5f * Ay * Time.deltaTime;
        }

        disp = new Vector3(newVx, Vy, 0.0f) * Time.deltaTime;
        Vx = newVx;

        transform.position = transform.position + disp;

        if (transform.position.y < 0.0f)
        {
            transform.position = new Vector3(transform.position.x, 0.0f, 0.0f);
        }
        if (transform.position.x < -3.5f)
        {
            transform.position = new Vector3(3.5f, transform.position.y, 0.0f);
        }
        if (transform.position.x > 3.5f)
        {
            transform.position = new Vector3(-3.5f, transform.position.y, 0.0f);
        }
    }
}
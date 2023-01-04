using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BasicMovement : MonoBehaviour
{
    public bool jumping;

    public float Kfr = 8.0f;
    public float frict;
    public const float gravity = -9.8f;

    public float playerMass = 70.0f;
    public float playerAcc = 1000.0f;
    public float playerSpd = 0.6f;

    public float inputHorizF;
    public float inputVertF = 0.0f;

    public float resultantHF;
    public float resultantVF;

    public float horizA = 0.0f;
    public float vertA = 0.0f;
    public float horizV = 0.0f;
    public int Dir;
    public float vertV = 0.0f;

    public Vector3 disp;

    void Update()
    {
        if (transform.position.y <= 0)
        {
            vertV = 0.0f;
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
            inputVertF = playerAcc*10;
        }
        else 
        {
            inputVertF = 0.0f;
        }

        inputHorizF = (Input.GetKey("a")  & !Input.GetKey("d") ? -1.0f :
                 !Input.GetKey("a") &  Input.GetKey("d") ? +1.0f : 0.0f) * playerAcc;

        if (horizV > 0)
        {
            Dir = 1;
        }
        if (horizV < 0)
        {
            Dir = -1;
        }
        if (horizV == 0)
        {
            Dir = 0;
        }
        frict = Kfr * playerMass * -Dir;

        if (Math.Abs(horizV) > playerSpd)
        {
            inputHorizF = 0;
        }
        if (jumping)
        {
            inputHorizF = 0;
            frict = 0;
        }

        resultantHF = inputHorizF + frict;
        resultantVF = inputVertF + gravity * playerMass;

        horizA = resultantHF / playerMass;
        vertA = resultantVF / playerMass;

        horizV += 0.5f * horizA * Time.deltaTime;
        
        if (transform.position.y >= 0)
        {
            vertV += 0.5f * vertA * Time.deltaTime;
        }

        disp = new Vector3(horizV*10, vertV, 0.0f) * Time.deltaTime;

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

        Debug.Log(horizV);
    }
}
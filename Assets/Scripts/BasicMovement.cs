using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public Animator animator;
    public bool jumping;
    public Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 acceleration = new Vector3(0.0f, 0.0f, 0.0f);
    public float horizA = 0.0f;
    public float vertA = 0.0f;
    public float horizV = 0.0f;
    public float vertV = 0.0f;
    public const float Kfr = 10f;
    public const float gravity = -9.8f;
    public float frict;
    public float playerM = 70.0f;
    public float inputVertF = 0.0f;
    public float inputHorizF;
    public float resultantHF;
    public float resultantVF;
    public Vector3 disp;
    public int Dir;

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
            inputVertF = 200f;
        }
        else 
        {
            inputVertF = 0.0f;
        }

        inputHorizF = (Input.GetKey("a")  & !Input.GetKey("d") ? -1.0f :
                 !Input.GetKey("a") &  Input.GetKey("d") ? +1.0f : 0.0f) * 20;

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
        frict = Kfr * playerM * -Dir;

        resultantHF = inputHorizF + frict;
        resultantVF = inputVertF + gravity * playerM;

        horizA = resultantHF / playerM;
        vertA = resultantVF / playerM;

        horizV += 0.5f * horizA * Time.deltaTime;
        
        if (transform.position.y >= 0)
        {
            vertV += 0.5f * vertA * Time.deltaTime;
        }

        disp = new Vector3(horizV, vertV, 0.0f) * Time.deltaTime;

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

        Debug.Log(transform.position.x);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float delaySeconds = 5f;

    public float time = 1.0f;
    public GameObject target;
    public float force = 10f;
    Rigidbody rb;
    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        Time.timeScale = time;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch l = new Launch();
            Nullable<Vector3> aimVector = l.Calculate(transform.position, target.transform.position, force, Physics.gravity);
            if (aimVector.HasValue)
            {
                rb.AddForce(aimVector.Value.normalized * force, ForceMode.VelocityChange);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            rb.isKinematic = true;
            transform.position = UnityEngine.Random.insideUnitSphere * 2;
            transform.position = new Vector3(transform.position.x, 0.05f, transform.position.z);
            rb.isKinematic = false;
        }
    }
}
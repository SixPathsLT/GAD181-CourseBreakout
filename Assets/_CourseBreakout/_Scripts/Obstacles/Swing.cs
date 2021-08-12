using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    Rigidbody rb;
    public float force;
    float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 32800);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5) 
        {
            rb.AddForce(transform.forward * force);
           // Debug.Log("EEEEE");
            timer = 0;
        }
    }
}

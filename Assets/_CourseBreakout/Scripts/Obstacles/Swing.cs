using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    Rigidbody rb;

    float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 500);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5) 
        {
            rb.AddForce(transform.forward * 100);
            Debug.Log("EEEEE");
            timer = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackwardBoost : MonoBehaviour
{
    [Range(100, 10000)]
    public float backwardboostspeed;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject backwardboostplatform = collision.gameObject;
        Rigidbody rb = backwardboostplatform.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.back * backwardboostspeed);
    }
}

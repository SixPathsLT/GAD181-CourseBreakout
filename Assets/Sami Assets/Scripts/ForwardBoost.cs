using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardBoost : MonoBehaviour
{
    [Range(100, 10000)]
    public float forwardboostspeed;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject forwardboostplatform = collision.gameObject;
        Rigidbody rb = forwardboostplatform.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * forwardboostspeed);
    }
}

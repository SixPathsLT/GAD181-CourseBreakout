using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackwardBoost : MonoBehaviour
{
    [Range(100, 10000)]
    public float backwardboostspeed;

    private void OnCollisionStay(Collision collision)
    {
        GameObject backwardboostplatform = collision.gameObject;
        Rigidbody rb = backwardboostplatform.GetComponent<Rigidbody>();
        rb.velocity += new Vector3(0, 0, -backwardboostspeed);
       // rb.AddForce(Vector3.back * backwardboostspeed);
    }
}

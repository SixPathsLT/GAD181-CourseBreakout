using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardBoost : MonoBehaviour
{
    [Range(1, 10000)]
    public float forwardboostspeed;

    private void OnCollisionStay(Collision collision)
    {
        GameObject forwardboostplatform = collision.gameObject;
        Rigidbody rb = forwardboostplatform.GetComponent<Rigidbody>();

        
       rb.velocity += new Vector3(0, 0, forwardboostspeed);
        //rb.AddForce(Vector3.forward * forwardboostspeed);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoostPlatform : MonoBehaviour
{
    [Range(100, 10000)]
    public float jumpboostheight;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject jumpboostplatform = collision.gameObject;
        Rigidbody rb = jumpboostplatform.GetComponent<Rigidbody>();
        // rb.AddForce(Vector3.up * jumpboostheight);

        
        if (collision.collider.CompareTag("Player"))
        {
           collision.collider.gameObject.GetComponent<PlayerControllerScript>().Jump(jumpboostheight, ForceMode.Force);
        }

    }
}

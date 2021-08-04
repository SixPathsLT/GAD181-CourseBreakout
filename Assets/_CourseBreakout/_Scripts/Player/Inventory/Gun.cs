using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public GameObject bullet;

    void Update()
    {
       if(Input.GetButtonDown("Fire1"))
       {
            Rigidbody rb = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
           rb.AddForce(transform.right * -42f, ForceMode.Impulse);
       } 
    }
}

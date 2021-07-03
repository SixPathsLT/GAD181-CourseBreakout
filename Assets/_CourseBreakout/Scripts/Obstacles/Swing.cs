using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    Rigidbody rb;

    public float speed;


    bool rotateR;
    bool rotateL;
    bool started = false;

    float timer;
    float maxtime;
    void Start()
    {

    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer <= 2) 
        {
            RotateRight();
            //started = true;
        }
        else if (timer <= 4) 
        {
            RotateLeft();
        }
        else 
        {
            timer = 0;
        }
    }


    public void RotateRight() 
    {
       // rotateR = false;
        //rotateL = false;
        /*if (!rotateR)
        {
            transform.Rotate(Vector3.right * speed * Time.deltaTime);
        }*/
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
      //  Invoke(nameof(RotateLeft), 3);
    }

    public void RotateLeft() 
    {
       // rotateR = true;
        //rotateL = true;
        //if (rotateL)
        //{
            transform.Rotate(Vector3.back * speed * Time.deltaTime);
        //}
       // Invoke(nameof(RotateRight), 3);
    }
}

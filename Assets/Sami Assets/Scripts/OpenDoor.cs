using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenDoor : MonoBehaviour
{
    public Animation hingehere;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void OnTriggerStay()
    {
        if (Input.GetKey(KeyCode.E))
        {
            hingehere.Play();
        }
    }
}

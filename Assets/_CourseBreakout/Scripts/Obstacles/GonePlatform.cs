using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GonePlatform : MonoBehaviour
{
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    // Toggle the Object's visibility each second.
    void Update()
    {
        // Find out whether current second is odd or even
        bool oddeven = Mathf.FloorToInt(Time.time) % 4 == 2;

        // Enable renderer accordingly
        rend.enabled = oddeven;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{

    public AudioSource shoot;
    public AudioSource jump;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            shoot.Play();
       // if (Input.GetKeyDown(KeyCode.Space))
       //    jump.Play();
    }
}

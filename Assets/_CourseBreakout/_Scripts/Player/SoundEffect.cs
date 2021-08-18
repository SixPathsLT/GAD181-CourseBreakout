using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{

    public AudioSource shoot;
    public AudioSource jump;
    PlayerControllerScript playerController;

    private void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerControllerScript>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!playerController.inTutorial && Input.GetKeyDown(KeyCode.Mouse0))
            shoot.Play();
       // if (Input.GetKeyDown(KeyCode.Space))
       //    jump.Play();
    }
}

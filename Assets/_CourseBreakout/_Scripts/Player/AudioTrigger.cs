using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{

    public AudioClip SoundToPlay;
    public float Volume;
    AudioSource audio;
    public bool alreadyPlayed = false;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider Player)
    {
        if (!alreadyPlayed & Player.tag == "Player")
        {
            audio.PlayOneShot(SoundToPlay, Volume);
            alreadyPlayed = true;
        }
    }
}

// Original If Statement
// if (!alreadyPlayed)

// Orginal OnTriggerEnter Function
// private void OnTriggerEnter(Collider Player)
// {
// if (Player.tag == "Player")
// {
//  audio.PlayOneShot(SoundToPlay, Volume);
//   alreadyPlayed = true;
// }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAudio : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public AudioClip Music;
    private AudioSource Audio;

    private void OnTriggerEnter(Collider Player)
    {
        if (Player.tag == "Player")
        {
            Audio = GetComponent<AudioSource>();
            Audio.clip = Music;
            Audio.loop = true;
            Audio.Play();
        }
    }
}

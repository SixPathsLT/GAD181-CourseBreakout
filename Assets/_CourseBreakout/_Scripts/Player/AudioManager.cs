using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip[] clips;
    AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.loop = true;
        //source = gameObject.AddComponent<AudioSource>();
       // source.volume = 0.188f;
    }


    public void PlayClip(string name) {
        bool foundClip = false;

        foreach (var clip in clips)
        {
            if (clip == null)
                continue;

            if (clip.name.Equals(name))
            {
                foundClip = true;
                source.clip = clip;
                source.Play();
                break;
            }
        }

        if (!foundClip)
            Debug.Log("Clip not found: " + name);
    }

}

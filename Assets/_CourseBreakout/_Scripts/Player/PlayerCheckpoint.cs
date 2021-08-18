using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    public GameObject CheckpointCube;

    Vector3 spawnPoint;

    public ParticleSystem HealthVFX;
    public ParticleSystem HealthVFX2;
    public ParticleSystem HealthVFX3;
    

    void Start()
    { 
        spawnPoint = gameObject.transform.position;
    }

    void Update()
    {
        if (gameObject.transform.position.y < -25f)
        {
             Respawn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            spawnPoint = other.gameObject.transform.position;
            Destroy(other.gameObject);
        }
    }

    public void Respawn() 
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<Rigidbody>().velocity = new Vector2(0, 0);
        gameObject.transform.position = spawnPoint + new Vector3(0, 1, 0);
        player.GetComponent<PlayerControllerScript>().yRotation = 0;
        player.GetComponent<PlayerControllerScript>().playerHealth = 300f;
        Debug.Log("Checkpoint Active");
    }
} 


// for our personal reference:
// public GameObject CheckpointCube2;
// spawnPoint = CheckpointCube2.transform.position;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    public GameObject CheckpointCube;

    Vector3 spawnPoint;

    public float playerHealth = 200;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -20f)
        {
            gameObject.transform.position = spawnPoint + new Vector3(0, 1, 0);
            Debug.Log("Checkpoint Active");
        }
        else if (playerHealth <= 0) 
        {
            gameObject.transform.position = spawnPoint + new Vector3(0, 1, 0);
            Debug.Log("Checkpoint Active");
            playerHealth = 200;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            spawnPoint = other.gameObject.transform.position;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("HealthKit"))
        {
            playerHealth += 50f;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyOrProjectilesOrBullets")) 
        {
            EnemyDamage(20);
        }
    }
    public void EnemyDamage(float amount) 
    {
        playerHealth -= amount;
    }
}


// for our personal reference:
// public GameObject CheckpointCube2;
// spawnPoint = CheckpointCube2.transform.position;
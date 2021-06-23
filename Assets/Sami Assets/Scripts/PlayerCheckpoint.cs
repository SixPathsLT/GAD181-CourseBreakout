using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    public GameObject CheckpointCube;

    Vector3 spawnPoint;

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
            gameObject.transform.position = spawnPoint + new Vector3 (0, 1, 0);
            Debug.Log("Checkpoint Active");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            spawnPoint = other.gameObject.transform.position;
            Destroy(other.gameObject);
        }
    }
}


// for our personal reference:
// public GameObject CheckpointCube2;
// spawnPoint = CheckpointCube2.transform.position;
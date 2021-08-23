using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCheckpoint : MonoBehaviour
{
    public GameObject CheckpointCube;

    Vector3 spawnPoint;

    public ParticleSystem HealthVFX;
    public ParticleSystem HealthVFX2;
    public ParticleSystem HealthVFX3;
    
    public GameObject livesRemaining;

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

    public void Respawn() {

        GameObject player = GameObject.FindWithTag("Player");

        PlayerControllerScript playerController = player.GetComponent<PlayerControllerScript>();

        player.GetComponent<Rigidbody>().velocity = new Vector2(0, 0);
        gameObject.transform.position = spawnPoint + new Vector3(0, 1, 0);
        playerController.yRotation = 0;
        playerController.playerData.ResetHealth();
        playerController.grappleHook.GetComponentInChildren<GrapplingGun>().StopGrapple();
        playerController.grappleHook.GetComponent<Item>().charges = 100f;
        playerController.playerData.remainingLives--;

        Transform heartTransform = livesRemaining.transform.GetChild(playerController.playerData.remainingLives);

        if (heartTransform != null)
            heartTransform.gameObject.GetComponent<Image>().CrossFadeColor(new Color32(85, 85, 85, 255), 0.7f, true, false);

        if (playerController.playerData.remainingLives < 1) {
            playerController.BackToMenu();
            return;
        }



        Debug.Log("Checkpoint Active");
    }
} 


// for our personal reference:
// public GameObject CheckpointCube2;
// spawnPoint = CheckpointCube2.transform.position;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySloth : MonoBehaviour
{
    public Transform player;

    [SerializeField] float distance;
    public float range;
    public float speed;


    // Update is called once per frame
    void Update()
    {
       
        distance = Vector3.Distance(transform.position, player.position);
        if (distance >= range) 
        {
           // Patrol();
        }
        if (distance <= range) 
        {
            AttackPlayer();
        }
        else 
        {
          //  Patrol();
        }
    }

    void AttackPlayer() 
    {
       
        if (player.GetComponent<PlayerControllerScript>().isGrounded)
        {
            transform.LookAt(player);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            player.GetComponent<PlayerCheckpoint>().playerHealth -= 10f;
        }
    }

    /*  void Patrol() 
      {
          if (isPatrolling)
          {
              transform.LookAt(patrolPoint);
              transform.Translate(Vector3.forward * speed * Time.deltaTime);
              if (enemyReached.z <= 1) 
              {
                  isPatrolling = false;
              }
          }
      } */
}

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
        if (distance <= range) 
        {
            AttackPlayer();
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

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            player.GetComponent<PlayerControllerScript>().playerHealth -= 0.1f;
        }
    }
}

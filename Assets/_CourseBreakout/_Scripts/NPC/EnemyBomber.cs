using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomber : MonoBehaviour
{
    public Transform player;

    [SerializeField] float distance;
    [SerializeField] float explodeRadius;
    [SerializeField] float explodeStrength;
    public float range;
    public float speed;

    void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);
        if (distance <= range)
        {
            EngagePlayer();
        }
        if (distance <= 3)
        {
            Explode();
        }

    }

    public void EngagePlayer()
    {
         if (player.GetComponent<PlayerControllerScript>().isGrounded)
         {
                transform.LookAt(player);
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
         }
       // GetComponentInChildren<Renderer>().material.color = new Color(1, 0, 0, 1);
    }
    public void Explode()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, explodeRadius);
        foreach(Collider near in collider) // (int i=0; i < collider.Length; i++)
        {
            Rigidbody body = near.GetComponent<Rigidbody>();
            if (body != null)
            {
                body.AddExplosionForce(explodeStrength, transform.position, explodeRadius, 3,ForceMode.Impulse);
            }
        }
        Destroy(transform.parent.gameObject, 0.1f);
    }
}

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

    float timer;

    public GameObject boomer;

    PlayerControllerScript health;

    private void Start()
    {;

    }
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);
        if (distance <= range)
        {
            EngagePlayer();
        }
        else
        {
            boomer.GetComponent<Renderer>().material.color = Color.black;
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
        timer += Time.deltaTime;
        //var colorWarn = boomer.GetComponent<Renderer>().material.color;
        if (timer <= 0.5f)
        {
            boomer.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (timer <= 1f)
        {
            boomer.GetComponent<Renderer>().material.color = Color.black;
        }
        else
        {
            timer = 0;
        }
        //boomer.GetComponent<Renderer>().material.color = new Color(255, 255, 255, 255);
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
                if (distance <= explodeRadius) 
                {
                    GameObject.Find("PlayerCharacter").GetComponent<PlayerControllerScript>().playerHealth -= 10;
                }
            }
        }
        Destroy(transform.gameObject, 0.2f);
    }
}

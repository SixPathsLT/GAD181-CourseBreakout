using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemies : MonoBehaviour
{

    public float dist;
    public float range;
    public float timeBetweenAttacks;
    public float upForce;


    public GameObject projectile;

    bool isAttacking = false;

    public Transform player;
    // Update is called once per frame
    private void Update()
    {
        dist = Vector3.Distance(player.position, transform.position);
        if (dist <= range) 
        {
            AttackPlayer();
        }
        else if (dist >= range) 
        {
            ResetAttack();
        }
    }

    public void AttackPlayer() 
    {
        transform.LookAt(player);
        if(!isAttacking)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 42f, ForceMode.Impulse);
            rb.AddForce(transform.up * upForce, ForceMode.Impulse);

            isAttacking = true;

            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    public void ResetAttack() 
    {
        isAttacking = false;
    }
}

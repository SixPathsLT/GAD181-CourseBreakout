using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemies : MonoBehaviour
{

    float dist; // doesnt need to be public :)
    public float range;
    public float timeBetweenAttacks;
    public float upForce;
    public float forwardForce;

    public GameObject projectile;

   public bool isAttacking = false;

   public Transform player;

    public ParticleSystem shootingVFX;

    private void Start()
    {
       
    }
    // Update is called once per frame
    private void Update()
    {
        if (shootingVFX != null)
            shootingVFX.transform.position = transform.position + Vector3.forward * 4;
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
           if (shootingVFX != null)
                shootingVFX.Play();
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 42f, ForceMode.Impulse); //was 42
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

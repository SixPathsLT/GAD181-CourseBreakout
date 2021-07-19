using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleSys : MonoBehaviour
{
    public ParticleSystem deathVFX;
    public float health = 100;

    private void Update()
    {
        deathVFX.transform.position = transform.position;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
            deathVFX.Play();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}

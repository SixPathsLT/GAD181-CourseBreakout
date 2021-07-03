using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleSys : MonoBehaviour
{
    public ParticleSystem deathVFX;
    public float health = 100;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleSys : MonoBehaviour
{
    public float health = 100;
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}

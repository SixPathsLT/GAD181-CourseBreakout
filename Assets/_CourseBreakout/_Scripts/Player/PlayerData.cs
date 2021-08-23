using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class PlayerData : ScriptableObject
{

    public float maxHealth = 200f;
    public float health;

    public int maxLives = 5;
    public int remainingLives;

   public void Start()
    {
        health = maxHealth;
        remainingLives = maxLives;
    }

    internal void ResetHealth()
    {
        health = maxHealth;
    }
}

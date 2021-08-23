using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class PlayerData : ScriptableObject
{

    public float maxHealth = 200f;
    public float health;

    void Start()
    {
        health = maxHealth;
    }

    internal void ResetHealth()
    {
        health = maxHealth;
    }
}

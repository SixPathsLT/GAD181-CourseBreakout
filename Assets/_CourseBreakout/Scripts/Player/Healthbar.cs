using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Image HealthBar;
    public float CurrentHealth;
    private float Maxhealth = 300;
    PlayerCheckpoint Player;
    void Start()
    {
        HealthBar = GetComponent<Image>();
        Player = FindObjectOfType<PlayerCheckpoint>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentHealth = Player.playerHealth;
        HealthBar.fillAmount = CurrentHealth / Maxhealth;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Image HealthBar;
    public float CurrentHealth;
    private float Maxhealth = 300;
   
    PlayerControllerScript Player;
    
    public Image healthWarn;

    float timer;

    bool isLow;

    void Start()
    {
        HealthBar = GetComponent<Image>();
        Player = FindObjectOfType<PlayerControllerScript>();
        isLow = false;
        healthWarn.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        CurrentHealth = Player.playerHealth;
        HealthBar.fillAmount = CurrentHealth / Maxhealth;
        if (CurrentHealth <= 200)
        {
            healthWarn.enabled = true;
        }
        if (CurrentHealth >= 200)
        {
            healthWarn.enabled = false;
        }
        if (CurrentHealth <= 100)
        {
            Warning();
           // healthWarn.enabled = false;
        }
    }
    public void Warning() 
    {
         if (timer <= 0.5f) 
         {
            healthWarn.enabled = false;
         }
         else if (timer <= 1)
         {
            healthWarn.enabled = true;
         }
         else 
         {
            timer = 0;
         }
    }
}


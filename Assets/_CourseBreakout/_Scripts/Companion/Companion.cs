using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float speed;
    public float spinSpeed;
    public float healSpinSpeed;
    public GameObject shieldScreen;
    
    float timer;
    float maxtime = 8f;
    float otherTimer;

    
    bool spinYN = false;
    public bool shieldPlayer;

    public ParticleSystem healthVFX;
    public ParticleSystem shieldVFX;

    bool isHealing = false;

   public GameObject shieldObject;

    void Start()
    {

    }

    public void ActivateShield()
    {
        if (!shieldPlayer)
        {
            shieldPlayer = true;
            shieldObject.SetActive(true);
            Invoke(nameof(ShieldEnd), 10);
            shieldScreen.SetActive(true);
            shieldVFX.Play();
       //     player.GetComponent<Collider>().isTrigger = true;
        }
    }

    public void ActivateHeal()
    {
        healthVFX.Play();
        isHealing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ActivateShield();
        }
        Shield();
      if (Input.GetKey(KeyCode.H))
      {
            ActivateHeal();
      }
        CompHeal();
        LookStraight();

        otherTimer += Time.deltaTime;
        if (otherTimer <= 1) 
        {
            transform.Translate(Vector3.down * 0.4f * Time.deltaTime);
        }
        if (otherTimer <= 2)
        {
            transform.Translate(Vector3.up * 0.2f * Time.deltaTime);
        }
        else 
        {
            otherTimer = 0;
        }
    }
    public void LookStraight() 
    {
        timer += Time.deltaTime;
        if (timer >= maxtime && !spinYN)
        { 
            transform.Rotate(new Vector3(0, -120, 0) * speed * Time.deltaTime);
            Invoke(nameof(StopSpin), 2);
        }

    }
    void StopSpin() 
    {
        spinYN = true;
    }

    public void Shield() 
    {
        if (shieldPlayer) 
        {
            transform.RotateAround(player.transform.position, Vector3.up, spinSpeed * Time.deltaTime);
        }
    }
    void ShieldEnd() 
    {
        shieldPlayer = false;

        shieldObject.SetActive(false);
        shieldScreen.SetActive(false);
        transform.position = player.transform.position;
        transform.position += player.transform.forward * 0.59f + player.transform.right * 0.4f + transform.up * 0.5f;
        transform.rotation = player.transform.rotation;
        player.GetComponent<Collider>().isTrigger = false;
    }

    public void CompHeal()
    {
        if (isHealing)
        {
            player.GetComponent<PlayerControllerScript>().playerHealth += 0.5f;
            Invoke(nameof(StopHeal), 4);
            transform.Rotate(new Vector3(0, 130, 0) * healSpinSpeed * Time.deltaTime);
            if (player.GetComponent<PlayerControllerScript>().playerHealth >= 300) 
            {
                player.GetComponent<PlayerControllerScript>().playerHealth = 300;
            }
        }
    }

    void StopHeal() 
    {
        isHealing = false;
        transform.rotation = player.transform.rotation;
    }
}

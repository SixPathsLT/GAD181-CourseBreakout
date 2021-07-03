using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public float speed;
    public float spinSpeed;
    public float healSpinSpeed;
    public GameObject shieldScreen;
    
    float timer;
    float maxtime = 8f;

    
    bool spinYN = false;
    bool shieldPlayer;

    public ParticleSystem healthVFX;

    bool isHealing = false;

    PlayerCheckpoint Player;
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
            Invoke(nameof(ShieldEnd), 5);
            shieldScreen.SetActive(true);
            player.GetComponent<Collider>().isTrigger = true;
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
      // transform.RotateAround(player.transform.position, Vector3.up, spinSpeed * Time.deltaTime);
        //transform.Rotate(new Vector3(20, 120, 0) * speed * Time.deltaTime);
      if (Input.GetKey(KeyCode.H))
      {
            ActivateHeal();
      }
        CompHeal();
        LookStraight();
    }
    public void LookStraight() 
    {
        timer += Time.deltaTime;
        if (timer >= maxtime && !spinYN)
        { 

            transform.Rotate(new Vector3(0, 120, 0) * speed * Time.deltaTime);
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
        transform.position = player.position;
        transform.position += player.transform.forward * 1.3f + player.transform.right * 0.7f + transform.up * 0.5f;
        transform.rotation = player.rotation;
        player.GetComponent<Collider>().isTrigger = false;
    }

    public void CompHeal()
    {
        if (isHealing)
        {
           // Player.playerHealth += amount;
            Invoke(nameof(StopHeal), 4);
            transform.Rotate(new Vector3(0, 130, 0) * healSpinSpeed * Time.deltaTime);
        }
    }

    void StopHeal() 
    {
        isHealing = false;
        transform.rotation = player.rotation;
    }
}

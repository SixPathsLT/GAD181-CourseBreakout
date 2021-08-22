using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public GameObject bullet;

    PlayerControllerScript playerController;

    private void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerControllerScript>();
    }

    void Update()
    {
        //if (GetComponentInParent<PauseMenu>().isPaused == false)
        //{
            if (!playerController.inTutorial && Input.GetButtonDown("Fire1"))
            {
                Rigidbody rb = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.right * -42f, ForceMode.Impulse);

                //Destroy(bullet, 5f);
            }
      //  }
    }
}

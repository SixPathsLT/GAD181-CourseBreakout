using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{

   /* GameObject[] enemies;
    Rigidbody rb;
    int timer;
    
    // Start is called before the first frame update
    void Start()
    {
        if (timer == 10)
        {
            rb = Instantiate(enemies[Random.Range(0, enemies.Length)], transform.forward, Quaternion.identity).GetComponent<Rigidbody>();
        }
    }*/

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerControllerScript>().GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            collider.transform.position = GameObject.Find("SpawnAtBoss").transform.position;
            FindObjectOfType<AudioManager>().PlayClip("BossBg");
          //  collider.transform.position = new Vector3(22.7f, 661.8f, -7138.8f);
        }
    }

}

//void OnTriggerEnter(Collider col)
//{
//    col.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), col.transform.position.y, Random.Range(-10.0f, 10.0f));
//}
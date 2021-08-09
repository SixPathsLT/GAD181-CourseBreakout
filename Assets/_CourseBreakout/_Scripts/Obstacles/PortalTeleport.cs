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

    void OnTriggerEnter(Collider Player)
    {
        if (Player.tag == "Player")
        {
            Player.transform.position = new Vector3(24.44f, 695.9f, -7113.86f);
        }
    }

}

//void OnTriggerEnter(Collider col)
//{
//    col.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), col.transform.position.y, Random.Range(-10.0f, 10.0f));
//}
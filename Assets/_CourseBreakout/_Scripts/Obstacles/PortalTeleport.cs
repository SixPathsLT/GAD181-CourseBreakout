using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider Player)
    {
        if (Player.tag == "Player")
        {
            Player.transform.position = new Vector3(84.1f, 121.7248f, -8243f);
        }
    }

}

//void OnTriggerEnter(Collider col)
//{
//    col.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), col.transform.position.y, Random.Range(-10.0f, 10.0f));
//}
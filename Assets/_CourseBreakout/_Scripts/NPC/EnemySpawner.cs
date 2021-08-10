using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemies;
    public Transform player;
    float timer;
    [SerializeField] float dist;
    [SerializeField] float range;
    void Start()
    {
        dist = Vector3.Distance(transform.position, player.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (dist <= range)
        {
            transform.LookAt(player);
            timer += Time.deltaTime;
            if(timer >= 5)
            {
                Rigidbody rb = Instantiate(Enemies[Random.Range(0, Enemies.Length)], transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 42f, ForceMode.Impulse);
                timer = 0;
            }
           /* else
            {
                timer = 0;
            }*/
        }
    }
}

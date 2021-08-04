using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyer : MonoBehaviour
{
    public Transform player;
    public float spinSpeed;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (GetComponentInChildren<ProjectileEnemies>().isAttacking) 
        {
            transform.LookAt(player); //right makes them go down.
          /*  if (timer <= 1)
            {
                transform.RotateAround(player.transform.position, Vector3.up, spinSpeed * Time.deltaTime);
            }
            else if (timer <= 2)
            {
                transform.RotateAround(player.transform.position, Vector3.down, spinSpeed * Time.deltaTime);
            }*/
            /*else*/ if (timer <= 1) // was 3 
            {
                transform.RotateAround(player.transform.position, Vector3.right, spinSpeed * Time.deltaTime);
                transform.RotateAround(player.transform.position, Vector3.down, spinSpeed * Time.deltaTime);
            } 
            else if (timer <= 2) //was 7
            {
                transform.RotateAround(player.transform.position, Vector3.left, spinSpeed * Time.deltaTime);
                transform.RotateAround(player.transform.position, Vector3.up, spinSpeed * Time.deltaTime);
            }
            else if (timer <= 3) //was 7
            {
                transform.RotateAround(player.transform.position, Vector3.left, spinSpeed * Time.deltaTime);
                transform.RotateAround(player.transform.position, Vector3.down, spinSpeed * Time.deltaTime);
            }
            else if (timer <= 4) //was 7
            {
                transform.RotateAround(player.transform.position, Vector3.right, spinSpeed * Time.deltaTime);
                transform.RotateAround(player.transform.position, Vector3.up, spinSpeed * Time.deltaTime);
            }
            else
            {
                timer = 0;
            } 
        }

    }

}

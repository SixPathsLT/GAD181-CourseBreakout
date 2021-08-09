using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyOrProjectilesOrBullets"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Poison"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Sloth"))
        {
            Destroy(other.gameObject);
        }

    }   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableRotate : MonoBehaviour
{
    public int speed;
    void Update()
    {
        transform.Rotate(new Vector3(0, 45, 0) * speed * Time.deltaTime);
    }
}

using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    GameObject platform;

    float speed = 2;
    float dirX = -1;
    
    Vector3 originalPosition;
    
    // Start is called before the first frame update
    void Start() {
        platform = GameObject.Find("BridgePlane");
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        speed = Random.Range(3, 15);
       
        if ((transform.position.x + 0.8) > platform.GetComponent<Renderer>().bounds.max.x)
            dirX = -1;
        else if ((transform.position.x - 0.4) < platform.GetComponent<Renderer>().bounds.min.x)
            dirX = 1;

        transform.position = new Vector3(transform.position.x + dirX * speed * Time.deltaTime, transform.position.y, transform.position.z);
             
    }
}

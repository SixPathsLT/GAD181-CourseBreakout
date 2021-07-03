using UnityEngine;

public class FloatingPlatformScript : MonoBehaviour
{

    public enum MovementType {
        Vertical, Horizontal, Hybrid, 
    }

    public MovementType movementType;

    [Header("Vertical Settings")]
    [Range(1, 50)]
    public float verticalSpeed = 10;
    public float minVerticalOffset = 10;
    public float maxVerticalOffset = 25;

    [Space(10)]

    [Header("Horizontal Settings")]
    [Range(1, 50)]
    public float horizontalSpeed = 10;
    public float minHorizontalOffset = 10;
    public float maxHorizontalOffset = 25;

    [Space(10)]

    [Header("Hybrid Settings")]
    [Range(1, 50)]
    public float hybridSpeed = 20;
    public float levitationMaxSpeed = 3;
    public float levitationMaxOffset = 30;
    public GameObject centerObject;

    float dir = -1;
    Vector3 originalPosition;

    float offset;
    
    // Start is called before the first frame update
    void Start() {
       
        switch (movementType)
        {
            case MovementType.Vertical:
                offset = Random.Range(minVerticalOffset, maxVerticalOffset);
                break;
            case MovementType.Horizontal:
                offset = Random.Range(minHorizontalOffset, maxHorizontalOffset);
                break;
            case MovementType.Hybrid:
                offset = Random.Range(1, levitationMaxOffset);
                break;
        }

         originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update() {

        switch(movementType)
        {
            case MovementType.Vertical:
                if (transform.position.y >= originalPosition.y + offset)
                    dir = -1;
                else if (transform.position.y <= originalPosition.y - offset)
                    dir = 1;

                transform.position = new Vector3(originalPosition.x, transform.position.y + dir * verticalSpeed * Time.deltaTime, originalPosition.z);

                break;
            case MovementType.Horizontal:
                if (transform.position.x >= originalPosition.x + offset)
                    dir = -1;
                else if (transform.position.x <= originalPosition.x - offset)
                    dir = 1;

                transform.position = new Vector3(transform.position.x + dir * horizontalSpeed * Time.deltaTime, originalPosition.y, originalPosition.z);
                break;
            case MovementType.Hybrid:
               if (centerObject == null)
                   return;

               Vector3 newPosition = new Vector3(centerObject.transform.position.x, dir, centerObject.transform.position.z);

               float levitationSpeed = Random.Range(1, levitationMaxSpeed);

               if (transform.position.y > originalPosition.y + offset)
                   dir = -1;
               else if (transform.position.y < originalPosition.y - offset)
                   dir = 1;
               
               transform.position = new Vector3(transform.position.x, transform.position.y + dir * levitationSpeed * Time.deltaTime, transform.position.z);
               transform.RotateAround(newPosition, Vector3.down, hybridSpeed * Time.deltaTime);
   
               break;
        }
    }


}

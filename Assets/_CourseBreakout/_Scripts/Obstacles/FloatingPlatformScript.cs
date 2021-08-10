using UnityEngine;

public class FloatingPlatformScript : MonoBehaviour
{

    public enum MovementType {
        Y_AXIS, X_AXIS, Z_AXIS, 
    }

    public MovementType movementType;
    
    [Range(1, 50)]
    public float speed = 10;
    public float minOffset = 10;
    public float maxOffset = 25;


    float dir = -1;
    Vector3 originalPosition;

    float offset;

    Rigidbody rigidBody;
    
    void Start() {
       originalPosition = transform.position;
       rigidBody = GetComponent<Rigidbody>();
       offset = Random.Range(minOffset, maxOffset);
    }
    
    void FixedUpdate() {
        switch(movementType)
        {
            case MovementType.Y_AXIS:
                if (transform.position.y >= originalPosition.y + offset)
                    dir = -1;
                else if (transform.position.y <= originalPosition.y - offset)
                    dir = 1;

                rigidBody.MovePosition(new Vector3(originalPosition.x, transform.position.y + dir * speed * Time.deltaTime, originalPosition.z));

                break;
            case MovementType.X_AXIS:
                if (transform.position.x >= originalPosition.x + offset)
                    dir = -1;
                else if (transform.position.x <= originalPosition.x - offset)
                    dir = 1;
                                
               rigidBody.MovePosition(new Vector3(transform.position.x + dir * speed * Time.deltaTime, originalPosition.y, originalPosition.z));
                break;
            case MovementType.Z_AXIS:
                if (transform.position.z >= originalPosition.z + offset)
                    dir = -1;
                else if (transform.position.z <= originalPosition.z - offset)
                    dir = 1;

               rigidBody.MovePosition(new Vector3(originalPosition.x, originalPosition.y, transform.position.z + dir * speed * Time.deltaTime));
                break;
        }
    }


}

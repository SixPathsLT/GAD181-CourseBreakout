using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{

    private LineRenderer lr;

    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    private float maxDistance = 100f;
    private SpringJoint joint;
    

    [SerializeField] float spring;
    [SerializeField] float damper;
    [SerializeField] float massScale;

    public Item grapplingHook;

    GameObject hitPointObject;

    void Awake()
    {
        grapplingHook = GetComponentInParent<Item>();
        lr = GetComponent<LineRenderer>();
        hitPointObject = new GameObject();
    }

    //Called after Update
    void LateUpdate()
    {
        DrawRope();
    }


    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    public void StartGrapple()
    {
       
        if (grapplingHook.charges < 1)
            return;

        //  grapplingHook.charges--;

     

        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable) && hit.collider.isTrigger == false && !hit.collider.CompareTag("BombBody"))
        {
            grapplingHook.SetUsingAbility(true);

            hitPointObject.transform.position = hit.point;
            hitPointObject.transform.SetParent(hit.transform);
            
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = hitPointObject.transform.position + (Vector3.up * 5);

            float distanceFromPoint = Vector3.Distance(player.position, hitPointObject.transform.position);


            //The distance grapple will try to keep from grapple point. 
             joint.maxDistance = distanceFromPoint *  0.25f;
            joint.minDistance = distanceFromPoint * 0.4f;// 0.8f;



            //Adjust these values to fit gameplay style.

            // How fast grappling hook will pull player
            joint.spring = spring;

            //How long it will stretch
            joint.damper = damper;

            // How fast player can move with grappling hook. 
            joint.massScale = massScale;


            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
        }
    }

    //[SerializedField]

    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    public void StopGrapple()
    {
        grapplingHook.SetUsingAbility(false);
        lr.positionCount = 0;
        Destroy(joint);
    }

    private Vector3 currentGrapplePosition;

    void DrawRope() { 
    

        //If not grappling, don't draw rope
        if (!joint) return;

        if (grapplingHook.charges < 1) {
            StopGrapple();
            return;
        }
       
        grapplingHook.ReduceCharges();

        bool hasControlsDown = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

       if (hasControlsDown) {
            joint.spring = spring;
            joint.damper = damper;
            joint.massScale = damper;
        } else {
            joint.spring /= 10;
            joint.damper /= 10;
            joint.massScale /= 10;
        }

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, hitPointObject.transform.position, Time.deltaTime * 8f);


        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return hitPointObject.transform.position;
    }
}

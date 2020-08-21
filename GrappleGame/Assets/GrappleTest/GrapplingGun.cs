using System;
using UnityEngine;

namespace Player
{
    
}
public class GrapplingGun : MonoBehaviour
{

    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public LayerMask obstructions;
    public Transform gunTip, gunDirection, player;
    [SerializeField] private float maxDistance = 1000f;
    private SpringJoint joint;
    [SerializeField] float grappleChangeSpeed;
    
    RaycastHit hit;
    public GameObject pointer;

    
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 0;
    }

    void Update()
    {
        if (Physics.Raycast(gunDirection.position, gunDirection.forward, out hit, maxDistance, whatIsGrappleable) && !IsGrappling())
        {
            grapplePoint = hit.point;
            pointer.transform.position = grapplePoint;

        }

        if (Input.GetKey(KeyCode.X)) { ChangeMaxDistance(grappleChangeSpeed);}
        if (Input.GetKey(KeyCode.Y)) { ChangeMaxDistance(-grappleChangeSpeed);}
        
        if (Input.GetMouseButtonDown(0) && !IsGrappling())
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonDown(0) && IsGrappling())
        {
            StopGrapple();
        }
        /*else if (Physics.Linecast(gunTip.position,grapplePoint,obstructions))
        {
            StopGrapple();
        }*/
        
    }

    //Called after Update
    void LateUpdate()
    {
        DrawRope();
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple()
    {
        
        if (Physics.Raycast(gunDirection.position, gunDirection.forward, out hit, maxDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint;
            joint.minDistance = 0;

            //Adjust these values to fit your game.
            joint.spring = Single.PositiveInfinity;
            joint.damper = 7f;
            joint.massScale = 4.5f;
            
            //renders line
            lr.positionCount = 2;
            
            currentGrapplePosition = gunTip.position;
            
            pointer.SetActive(false);
            
        }
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    public void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
        
        pointer.SetActive(true);

    }

    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        //If not grappling, don't draw rope
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }

    public void ChangeMaxDistance(float addMaxDistance)
    {
        joint.maxDistance += addMaxDistance;

        if (joint.maxDistance < 1)
        {
            joint.maxDistance = 1;
        }
    }
}

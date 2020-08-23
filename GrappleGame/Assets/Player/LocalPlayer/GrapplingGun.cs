using System;
using Server;
using UnityEngine;

namespace Player
{
    
}
public class GrapplingGun : MonoBehaviour
{
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public LayerMask obstructions;
    public Transform gunDirection, player;
    [SerializeField] private float maxDistance = 1000f;
    private SpringJoint joint;
    [SerializeField] float grappleChangeSpeed;
    
    RaycastHit hit;
    public GameObject pointer;
    
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

            GameManager.players[Client.instance.myId].isGrappling = true;
            GameManager.players[Client.instance.myId].grapplePoint = grapplePoint;
            GameManager.players[Client.instance.myId].maxDistanceFromGrapple = distanceFromPoint;
            pointer.SetActive(false);
            
            ClientSend.GrappleUpdate(grapplePoint, true, distanceFromPoint);
        }
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    public void StopGrapple()
    {
        Destroy(joint);
        
        GameManager.players[Client.instance.myId].isGrappling = false;
        pointer.SetActive(true);

        ClientSend.GrappleUpdate(grapplePoint, false, 1);
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

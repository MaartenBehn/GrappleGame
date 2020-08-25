using System;
using Server;
using UnityEngine;

namespace Player.LocalPlayer
{
    public class GrapplingGun : MonoBehaviour
    {
        
        
        [SerializeField] private Transform gunDirection, player;
        [SerializeField] private GameObject pointer;
        [SerializeField] private LayerMask whatIsGrappleable, obstructions;
        [SerializeField] private float maxDistance = 1000f, grappleChangeSpeed;

        private Vector3 grapplePoint;
        private bool grappling;

        private RaycastHit hit;
        private bool hitting;
        private SpringJoint joint;
        private bool snappingGrapplePoint;
        private bool playerGrapplePoint;

        private void Update()
        {
            hitting = Physics.Raycast(gunDirection.position, gunDirection.forward, out hit, maxDistance, whatIsGrappleable);
            try
            {
                if (hit.transform.CompareTag("EasyGrapple"))
                {
                    hit.point = hit.transform.position;
                    snappingGrapplePoint = true;
                }
                else if(hit.transform.CompareTag("Player")) 
                {
                    hit.point = hit.transform.position;
                    playerGrapplePoint = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            GameManager.players[Client.instance.myId].grapplePoint = grapplePoint;

            if (Input.GetKey(KeyCode.X)) { ChangeMaxDistance(grappleChangeSpeed);}
            if (Input.GetKey(KeyCode.Y)) { ChangeMaxDistance(-grappleChangeSpeed);}
        
            if (Input.GetMouseButtonDown(0) && !grappling)
            {
                StartGrapple();
            }
            else if (Input.GetMouseButtonDown(0) && grappling)
            {
                StopGrapple();
            }
            /*else if (Physics.Linecast(gunTip.position,grapplePoint,obstructions))
            {
                StopGrapple();
            }*/

            if (!grappling && hitting)
            {
                pointer.SetActive(true);
                pointer.transform.position = hit.point;
            }
            else
            {
                pointer.SetActive(false);
            }
            snappingGrapplePoint = false;
            playerGrapplePoint = false;
        }
    
        /// <summary>
        /// Call whenever we want to start a grapple
        /// </summary>
        void StartGrapple()
        {
            if(!hitting) return;
            
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            grapplePoint = hit.point;
            joint.connectedAnchor = hit.point;

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = Vector3.Distance(player.position, grapplePoint);
            joint.minDistance = 0;

            //Adjust these values to fit your game.
            joint.spring = 10000;
            joint.damper = 7f;
            joint.massScale = 4.5f;
            
            grappling = true;
            
            UpdateServer();
        }


        /// <summary>
        /// Call whenever we want to stop a grapple
        /// </summary>
        public void StopGrapple()
        {
            Destroy(joint);
            grappling = false;
            grapplePoint = Vector3.zero;

            UpdateServer();
        }
        public void ChangeMaxDistance(float addMaxDistance)
        {
            joint.maxDistance += addMaxDistance;

            if (joint.maxDistance < 1)
            {
                joint.maxDistance = 1;
            }
            
            UpdateServer();
        }

        public void UpdateServer()
        {
            if (snappingGrapplePoint)
            {
                GameManager.players[Client.instance.myId].isGrappling = grappling;
                GameManager.players[Client.instance.myId].maxDistanceFromGrapple = joint.maxDistance;
                
                ClientSend.GrappleUpdate(hit.transform.name, grappling, joint.maxDistance);
                return;
            }
            else if (playerGrapplePoint)
            {
                GameManager.players[Client.instance.myId].isGrappling = grappling;
                GameManager.players[Client.instance.myId].maxDistanceFromGrapple = joint.maxDistance;

                ClientSend.GrappleUpdate(hit.transform.GetComponentInParent<PlayerManager>().id.ToString(), grappling, joint.maxDistance);
                return;
            }
            GameManager.players[Client.instance.myId].isGrappling = grappling;
            GameManager.players[Client.instance.myId].maxDistanceFromGrapple = joint.maxDistance;

            ClientSend.GrappleUpdate(grapplePoint, grappling, joint.maxDistance);
        }
    }
}

using SharedFiles.Lobby;
using UnityEngine;

namespace Player.Trooper
{
	public class Trooper : MonoBehaviour
    {
        public PlayerManager player;
        
		[SerializeField] private Animator animator;
                
        public Vector3 velocity;
        public bool grounded;
        private static readonly int Grounded = Animator.StringToHash("Grounded");
        private static readonly int Velocity = Animator.StringToHash("Velocity");

        public LineRenderer lr;
        public Transform grappleTip;
        public bool isGrappling = false;
        public string grappleObjectId; // <type id>
        public Vector3 grapplePoint;
        public float maxDistanceFromGrapple;
        
        private void FixedUpdate()
        {
            // Updating animator
            animator.SetBool(Grounded, grounded);
            animator.SetFloat(Velocity, velocity.magnitude);
        }
        
        void LateUpdate()
        {
            UpadteGrapple();
        }
        
        void UpadteGrapple()
        {
            //If not grappling, don't draw rope
            if (!isGrappling)
            {
                lr.positionCount = 0;
                return;
            }

            Vector3 absGrapplePoint = Vector3.zero;
            string[] parts = grappleObjectId.Split(' ');
            switch (parts[0])
            {
                case "snap":
                    absGrapplePoint = LobbyData.instance.snappingObjects[int.Parse(parts[1])].transform.position + grapplePoint;
                    break;
                case "player":
                    absGrapplePoint = GameManager.players[int.Parse(parts[1])].trooper.transform.position + grapplePoint;
                    break;
                default:
                    absGrapplePoint = grapplePoint;
                    break;
            }

            lr.material.SetFloat("Mix",Vector3.Distance(grappleTip.position, grapplePoint) / maxDistanceFromGrapple);
            lr.positionCount = 2;
            lr.SetPosition(0, grappleTip.position);
            lr.SetPosition(1, absGrapplePoint);
        }
	}
}
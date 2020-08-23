using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public int id;
        public string username;

        public Vector3 velocity;
        public bool grounded;
        private static readonly int Grounded = Animator.StringToHash("Grounded");
        private static readonly int Velocity = Animator.StringToHash("Velocity");

        public LineRenderer lr;
        public Transform grappleTip;
        public bool isGrappling = false;
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
            lr.material.SetFloat("Mix",Vector3.Distance(grappleTip.position, grapplePoint) / maxDistanceFromGrapple);
            lr.positionCount = 2;
            lr.SetPosition(0, grappleTip.position);
            lr.SetPosition(1, grapplePoint);
        }
    }
}

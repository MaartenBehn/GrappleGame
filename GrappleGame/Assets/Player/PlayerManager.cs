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

        private void FixedUpdate()
        {
            // Updating animator
            animator.SetBool(Grounded, grounded);
            animator.SetFloat(Velocity, velocity.magnitude);
        }
    }
}

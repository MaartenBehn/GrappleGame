using UnityEngine;

namespace Player
{
	public class Trooper : MonoBehaviour
	{
		public PlayerManager player;
		public Vector3 velocity;
		public bool grounded;
    
		public bool isGrappling;
		public string grappleObjectId;
		public Vector3 grapplePoint;
		public float distanceFromGrapple;

		public void UpdateTransform(Vector3 position, Quaternion rotation, Vector3 velocity, bool grounded)
		{
			Transform transform1 = transform;
			transform1.position = position;
			transform1.rotation = rotation;
			this.velocity = velocity;
			this.grounded = grounded;
		
			ServerSend.TrooperTransformUpdate(this);
		}

		public void GrappleUpdate(string grappleObjectId, bool isGrappling, Vector3 grapplePoint, float distanceFromGrapple)
		{
			this.isGrappling = isGrappling;
			this.grappleObjectId = grappleObjectId;
			this.grapplePoint = grapplePoint;
			this.distanceFromGrapple = distanceFromGrapple;

			ServerSend.TrooperGrappleUpdate(this);
		}
	}
}
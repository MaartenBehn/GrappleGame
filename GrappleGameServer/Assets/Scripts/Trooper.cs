using UnityEngine;

public class Trooper : MonoBehaviour
{
	public Player player;
	public Vector3 velocity;
	public bool grounded;
    
	public bool isGrappling;
	public string grappleObjectId;
	public Vector3 grapplePoint;
	public float distanceFromGrapple;
		
	/// <summary>Processes player input and moves the player.</summary>
	public void FixedUpdate()
	{
		ServerSend.ClientTransformUpdate(player);
	}
    
	public void SetTransform(Vector3 position, Quaternion rotation, Vector3 velocity, bool grounded)
	{
		Transform transform1 = transform;
		transform1.position = position;
		transform1.rotation = rotation;
		this.velocity = velocity;
		this.grounded = grounded;
	}

	public void GrappleUpdate(string grappleObjectId, bool isGrappling, Vector3 grapplePoint, float distanceFromGrapple)
	{
		this.isGrappling = isGrappling;
		this.grappleObjectId = grappleObjectId;
		this.grapplePoint = grapplePoint;
		this.distanceFromGrapple = distanceFromGrapple;

		ServerSend.ClientGrappleUpdate(player);
	}
}
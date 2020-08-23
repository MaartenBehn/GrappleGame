using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public string username;
    public Vector3 velocity;
    public bool grounded;
    public bool isGrappling;
    public Vector3 grapplePoint;
    public float distanceFromGrapple;

    public void Initialize(int id, string username)
    {
        this.id = id;
        this.username = username;
    }

    /// <summary>Processes player input and moves the player.</summary>
    public void FixedUpdate()
    {
        ServerSend.ClientTransformUpdate(this);
    }
    
    public void SetTransform(Vector3 position, Quaternion rotation, Vector3 velocity, bool grounded)
    {
        Transform transform1 = transform;
        transform1.position = position;
        transform1.rotation = rotation;
        this.velocity = velocity;
        this.grounded = grounded;
    }

    public void GrappleUpdate(bool isGrappling, Vector3 grapplePoint, float distanceFromGrapple)
    {
        this.isGrappling = isGrappling;
        this.grapplePoint = grapplePoint;
        this.distanceFromGrapple = distanceFromGrapple;

        ServerSend.ClientGrappleUpdate(this);
    }

    public void Disconnect()
    {
        ServerSend.PlayerLeave(this);
    }
}

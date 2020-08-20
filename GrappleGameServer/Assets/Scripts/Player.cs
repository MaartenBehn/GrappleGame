using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public string username;
    public Vector3 velocity;
    public bool grounded;

    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
    }

    /// <summary>Processes player input and moves the player.</summary>
    public void FixedUpdate()
    {
        ServerSend.ClientTransformUpdate(this);
    }


    /// <summary>Updates the player input with newly received input.</summary>
    /// <param name="_inputs">The new key inputs.</param>
    /// <param name="_rotation">The new rotation.</param>
    public void SetTransform(Vector3 position, Quaternion rotation, Vector3 velocity, bool grounded)
    {
        transform.position = position;
        transform.rotation = rotation;
        this.velocity = velocity;
        this.grounded = grounded;
    }

    public void Disconnect()
    {
        ServerSend.PlayerLeave(this);
    }
}

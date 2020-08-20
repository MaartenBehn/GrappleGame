using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public string username;

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
    public void SetTransform(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    public void Disconnect()
    {
        ServerSend.PlayerLeave(this);
    }
}

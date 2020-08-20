using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Animator animatior;

    public int id;
    public string username;

    public Vector3 velocity;
    public bool grounded;

    private void FixedUpdate()
    {
        // Updating animator
        animatior.SetBool("Grounded", grounded);
        animatior.SetFloat("Velocity", velocity.magnitude);
    }
}

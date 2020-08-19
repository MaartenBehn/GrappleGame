using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GrappleTestPlayer : MonoBehaviour
{
    [SerializeField] Transform modelTransform;
    [SerializeField] Transform camTransform;
    [SerializeField] CinemachineFreeLook cinemachineFreeLook;
    private Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public float horizontalRotation;
    private float verticalRotation;
    public float jetpackSpeed;
    bool grounded = false;
    
    void Update()
    {
        cinemachineFreeLook.m_YAxis.m_InputAxisName = grounded ? "Mouse Y" : "";

        if (!grounded)
        {
            float _mouseVertical = -Input.GetAxis("Mouse Y");
            float _mouseHorizontal = Input.GetAxis("Mouse X");

            verticalRotation += _mouseVertical * sensitivity * Time.deltaTime;
            horizontalRotation += _mouseHorizontal * sensitivity * Time.deltaTime;

            transform.localRotation = Quaternion.Euler(0f, horizontalRotation, verticalRotation);

            Vector3 input = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                input.x += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                input.x -= 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                input.z += 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                input.z -= 1;
            }

            input = transform.rotation * -input;

            Vector3 speed = input * jetpackSpeed;
            rigidbody.AddForce(speed);
        }
    }

    public Vector3 normal;
    public float magnetBootsForce = 10;
    public float walkSpeed = 10;
    public float sensitivity = 100;

    void OnCollisionEnter(Collision collisionInfo)
    {
        normal = collisionInfo.contacts[0].normal;
        grounded = true;
    }
    void OnCollisionStay(Collision collisionInfo)
    {
        normal = collisionInfo.contacts[0].normal;

        float _mouseHorizontal = Input.GetAxis("Mouse X");
        horizontalRotation += _mouseHorizontal * sensitivity * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);
        transform.rotation = Quaternion.FromToRotation(transform.up, normal) * transform.rotation;

        Vector3 input = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            input.x += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            input.x -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            input.z += 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            input.z -= 1;
        }

        input = transform.rotation * -input;

        Vector3 speed = input * walkSpeed;
        rigidbody.velocity = speed;
        grounded = true;
    }

    void OnTriggerStay(Collider other)
    {
        rigidbody.AddForce(-normal * magnetBootsForce);
    }

    void OnTriggerExit(Collider other)
    {
        normal = Vector3.zero;
        grounded = false;
    }
}

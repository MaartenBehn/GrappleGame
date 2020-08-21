using Cinemachine;
using UnityEngine;

namespace GrappleTest
{
    public class GrappleTestPlayer : MonoBehaviour
    {
        [SerializeField] CinemachineFreeLook cinemachineFreeLook;
        new Rigidbody rigidbody;
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        public float horizontalRotation;
        private float verticalRotation;
        bool grounded = false;
        Vector3 groundNormal;

        [SerializeField] float sensitivity = 100;
        [SerializeField] float jetpackSpeed;

        Vector3 input;
        void Update()
        {
            cinemachineFreeLook.m_YAxis.m_InputAxisName = grounded ? "Mouse Y" : "";

            Vector3 input = Vector3.zero;
            if (Input.GetKey(KeyCode.W)){input.x += 1;}
            if (Input.GetKey(KeyCode.S)){input.x -= 1;}
            if (Input.GetKey(KeyCode.A)){input.z += 1;}
            if (Input.GetKey(KeyCode.D)){input.z -= 1;}
            input = transform.rotation * -input;

            if (!grounded)
            {
                float _mouseVertical = -Input.GetAxis("Mouse Y");
                float _mouseHorizontal = Input.GetAxis("Mouse X");

                verticalRotation += _mouseVertical * sensitivity * Time.deltaTime;
                horizontalRotation += _mouseHorizontal * sensitivity * Time.deltaTime;

                transform.localRotation = Quaternion.Euler(0f, horizontalRotation, verticalRotation);

                Vector3 speed = input * jetpackSpeed;
                rigidbody.AddForce(speed);
            }
        }

        [SerializeField] float magnetBootsForce = 10;
        [SerializeField] float walkSpeed = 10;

        void OnCollisionEnter(Collision collisionInfo)
        {
            groundNormal = collisionInfo.contacts[0].normal;
            grounded = true;
        }
        void OnCollisionStay(Collision collisionInfo)
        {
            groundNormal = collisionInfo.contacts[0].normal;

            float _mouseHorizontal = Input.GetAxis("Mouse X");
            horizontalRotation += _mouseHorizontal * sensitivity * Time.deltaTime;

            transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);
            transform.rotation = Quaternion.FromToRotation(transform.up, groundNormal) * transform.rotation;

            Vector3 speed = input * walkSpeed;
            rigidbody.velocity = speed;
            grounded = true;
        }

        void OnTriggerStay(Collider other)
        {
            rigidbody.AddForce(-groundNormal * magnetBootsForce);
        }

        void OnTriggerExit(Collider other)
        {
            groundNormal = Vector3.zero;
            grounded = false;
        }
    }
}

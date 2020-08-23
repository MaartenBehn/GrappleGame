using Cinemachine;
using Server;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] CinemachineFreeLook cinemachineFreeLook;
        PlayerManager playerManager;
        new Rigidbody rigidbody;
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            playerManager = GetComponent<PlayerManager>();
        }

        bool grounded = false;
        Vector3 groundNormal;

        [SerializeField] float sensitivity = 100f;
        [SerializeField] float jetpackSpeed = 1f;
        [SerializeField] float magnetBootsForce = 10f;
        [SerializeField] float walkSpeed = 10f;
        
        void FixedUpdate()
        {
            // Getting movment inputs
            Vector3 input = Vector3.zero;
            if (Input.GetKey(KeyCode.W)) { 
                input.z -= 1;
            }
            if (Input.GetKey(KeyCode.S)) { input.z += 1; }
            if (Input.GetKey(KeyCode.A)) { input.x += 1; }
            if (Input.GetKey(KeyCode.D)) { input.x -= 1; }
            input = transform.rotation * -input;

            // Performing Movment and Rotation
            if (grounded)
            {
                // Rotation on Ground
                float mouseHorizontal = Input.GetAxis("Mouse X");

                var rotation = transform.rotation;
                rotation = Quaternion.FromToRotation(rotation * Vector3.up, groundNormal) * rotation;
                transform.rotation = rotation;
                transform.Rotate(0f, mouseHorizontal * sensitivity * Time.deltaTime, 0f);

                // Movment on Ground
                Vector3 speed = input * walkSpeed;
                rigidbody.velocity = speed;
            }
            else
            {
                // Rotaion in Space
                float mouseVertical = -Input.GetAxis("Mouse Y");
                float mouseHorizontal = Input.GetAxis("Mouse X");

                transform.Rotate(mouseVertical * sensitivity * Time.deltaTime, mouseHorizontal * sensitivity * Time.deltaTime, 0f);

                // Movment in Space
                Vector3 speed = input * jetpackSpeed;
                rigidbody.AddForce(speed);
            }

            // Updating depending varibles
            cinemachineFreeLook.m_YAxis.m_InputAxisName = grounded ? "Mouse Y" : "";

            playerManager.grounded = grounded;
            playerManager.velocity = rigidbody.velocity;

            // Sending transform to Server
            ClientSend.TransformUpdate();
        }

        void OnCollisionEnter(Collision collisionInfo)
        {
            SetGroundNormal(collisionInfo);
            SetGrounded(true);
        }
        void OnCollisionStay(Collision collisionInfo)
        {
            SetGroundNormal(collisionInfo);
            SetGrounded(true);
        }

        void OnTriggerStay(Collider other)
        {
            rigidbody.AddForce(-groundNormal * magnetBootsForce);
        }

        void OnTriggerExit(Collider other)
        {
            SetGrounded(false);
        }

        void SetGroundNormal(Collision collisionInfo)
        {
            if (collisionInfo.gameObject.transform.tag.Equals("InverseSpehere"))
            {
                groundNormal = ((collisionInfo.gameObject.transform.position - transform.position).normalized);
            }
            else
            {
                groundNormal = collisionInfo.contacts[0].normal;
            }
        }

        void SetGrounded(bool grounded)
        {
            if (grounded)
            {
                this.grounded = true;
            }
            else
            {
                this.grounded = false;
                groundNormal = Vector3.zero;
                cinemachineFreeLook.m_YAxis.Value = 0.5f;
            }
        }
    }
}

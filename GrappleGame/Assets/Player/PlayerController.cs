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
                input.x += 1;
            }
            if (Input.GetKey(KeyCode.S)) { input.x -= 1; }
            if (Input.GetKey(KeyCode.A)) { input.z += 1; }
            if (Input.GetKey(KeyCode.D)) { input.z -= 1; }
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

                transform.Rotate(0f, mouseHorizontal * sensitivity * Time.deltaTime, mouseVertical * sensitivity * Time.deltaTime);

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
            grounded = true;
        }
        void OnCollisionStay(Collision collisionInfo)
        {
            SetGroundNormal(collisionInfo);
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
    }
}

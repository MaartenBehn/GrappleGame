using System.Net;
using SharedFiles.Utility;
using UI;
using UnityEngine;

namespace Server
{
    public class ClientHandle : MonoBehaviour
    {
        public static void ServerConnection(Packet packet)
        {
            int myId = packet.ReadInt();

            Debug.Log("Established TCP connection");
            Client.instance.myId = myId;
            ClientSend.ServerConnectionReceived();

            // Now that we have the client's id, connect UDP
            Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
        }
        
        public static void GameEnterRejected(Packet packet)
        {
            string message = packet.ReadString();
            Debug.Log(message);
            
            Client.instance.Disconnect();
            UIManager.instance.SwitchPanel(PanelType.startPanel);
        }
        
        public static void LobbyChange(Packet packet)
        {
            string lobbyName = packet.ReadString();
            
            GameManager.instance.LobbyChange(lobbyName);
        }

        public static void PlayerEnter(Packet packet)
        {
            int id = packet.ReadInt();
            string username = packet.ReadString();
            Vector3 position = packet.ReadVector3();
            Quaternion rotation = packet.ReadQuaternion();

            GameManager.instance.PlayerEnter(id, username, position, rotation);
        }

        public static void PlayerLeave(Packet packet)
        {
            int id = packet.ReadInt();

            Destroy(GameManager.players[id].gameObject);
            GameManager.players.Remove(id);
        }

        public static void ClientTransformUpdate(Packet packet)
        {
            int id = packet.ReadInt();
            Vector3 position = packet.ReadVector3();
            Quaternion rotation = packet.ReadQuaternion();
            Vector3 velocity = packet.ReadVector3();
            bool grounded = packet.ReadBool();

            GameManager.players[id].transform.position = position;
            GameManager.players[id].transform.rotation = rotation;
            GameManager.players[id].velocity = velocity;
            GameManager.players[id].grounded = grounded;
        }
        
        public static void ClientGrappleUpdate(Packet packet)
        {
            int id = packet.ReadInt();
            bool isGrappling = packet.ReadBool();
            string objectId = packet.ReadString();
            Vector3 position = packet.ReadVector3();
            float maxDistanceFromGrapple = packet.ReadFloat();

            GameManager.players[id].isGrappling = isGrappling;
            GameManager.players[id].grappleObjectId = objectId;
            GameManager.players[id].grapplePoint = position;
            GameManager.players[id].maxDistanceFromGrapple = maxDistanceFromGrapple;

        }
    }
}

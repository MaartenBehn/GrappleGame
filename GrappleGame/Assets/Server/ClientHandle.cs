using System.Net;
using UnityEngine;
using Utility;

namespace Server
{
    public class ClientHandle : MonoBehaviour
    {
        public static void ServerConnection(Packet packet)
        {
            string msg = packet.ReadString();
            int myId = packet.ReadInt();

            Debug.Log($"Message from server: {msg}");
            Client.instance.myId = myId;
            ClientSend.ServerConnectionReceived();

            // Now that we have the client's id, connect UDP
            Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
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
    }
}

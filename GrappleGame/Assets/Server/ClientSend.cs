using UI;
using UnityEngine;
using Utility;

namespace Server
{
    public class ClientSend : MonoBehaviour
    {
        /// <summary>Sends a packet to the server via TCP.</summary>
        /// <param name="packet">The packet to send to the sever.</param>
        private static void SendTcpData(Packet packet)
        {
            packet.WriteLength();
            Client.instance.tcp.SendData(packet);
        }

        /// <summary>Sends a packet to the server via UDP.</summary>
        /// <param name="packet">The packet to send to the sever.</param>
        private static void SendUdpData(Packet packet)
        {
            packet.WriteLength();
            Client.instance.udp.SendData(packet);
        }

        #region Packets
        /// <summary>Lets the server know that the welcome message was received.</summary>
        public static void ServerConnectionReceived()
        {
            using (Packet packet = new Packet((int)ClientPackets.serverConnectionReceived))
            {
                packet.Write(Client.instance.myId);
                packet.Write(UIManager.gameSettings.playerName);

                SendTcpData(packet);
            }
        }

        /// <summary>Sends player input to the server.</summary>
        public static void TransformUpdate()
        {
            using (Packet packet = new Packet((int)ClientPackets.transformUpdate))
            {
                packet.Write(GameManager.players[Client.instance.myId].transform.position);
                packet.Write(GameManager.players[Client.instance.myId].transform.rotation);
                packet.Write(GameManager.players[Client.instance.myId].velocity);
                packet.Write(GameManager.players[Client.instance.myId].grounded);

                SendUdpData(packet);
            }
        }
        
        public static void GrappleUpdate(string objectId, Vector3 pos, bool isGrappling, float distanceFromPoint)
        {
            using (Packet packet = new Packet((int)ClientPackets.grappleUpdate))
            {
                packet.Write(isGrappling);
                packet.Write(objectId);
                packet.Write(pos);
                packet.Write(distanceFromPoint);

                SendUdpData(packet);
            }
        }
        #endregion
    }
}

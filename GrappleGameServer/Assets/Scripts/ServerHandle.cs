
using UnityEngine;

public static class ServerHandle
{
    public static void GameEnterReqest(int fromClient, Packet packet)
    {
        int clientIdCheck = packet.ReadInt();
        string username = packet.ReadString();
        string password = packet.ReadString();

        if (NetworkManager.instance.password != "" && NetworkManager.instance.password != password)
        {
            Debug.Log($"{Server.clients[fromClient].tcp.socket.Client.RemoteEndPoint} has entered a wrong password!");
            ServerSend.GameEnterRejected(fromClient, "Wrong Password!");
            return;
        }
        
        if (Server.conectedClinets >= Server.MaxPlayers)
        {
            Debug.Log($"{Server.clients[fromClient].tcp.socket.Client.RemoteEndPoint} has entered a wrong password!");
            ServerSend.GameEnterRejected(fromClient, "Server full");
            return;
        }
        
        if (fromClient != clientIdCheck)
        {
            Debug.Log($"Player \"{username}\" (ID: {fromClient}) has assumed the wrong client ID ({clientIdCheck})!");
            ServerSend.GameEnterRejected(fromClient, "Your Id does not match the server Id!");
            return;
        }
        Debug.Log($"{Server.clients[fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {fromClient}.");
        
        Server.clients[fromClient].SendIntoGame(username);
    }

    public static void TransformUpdate(int fromClient, Packet packet)
    {
        Vector3 position = packet.ReadVector3();
        Quaternion rotation = packet.ReadQuaternion();
        Vector3 velocity = packet.ReadVector3();
        bool grounded = packet.ReadBool();

        Server.clients[fromClient].player.SetTransform(position, rotation, velocity, grounded);
    }
    
    public static void GrappleUpdate(int fromClient, Packet packet)
    {
        bool isGrappling = packet.ReadBool();
        string objectId = packet.ReadString();
        Vector3 position = packet.ReadVector3();
        float distanceFromGrapple = packet.ReadFloat();
        
        Server.clients[fromClient].player.GrappleUpdate(objectId, isGrappling, position, distanceFromGrapple);
    }
}

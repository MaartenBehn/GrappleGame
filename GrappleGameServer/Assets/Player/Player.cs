
using SharedFiles.Utility;
using UnityEngine;

public class Player
{
    public string username;
    public Client client;
    public PlayerState state;
    public Trooper trooper;
    public void Disconnect()
    {
        ServerSend.PlayerLeave(this);
    }
}

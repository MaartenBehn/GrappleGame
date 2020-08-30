
public class Player
{
    public string username;
    public Client client;
    public Trooper trooper;
    public void Disconnect()
    {
        ServerSend.PlayerLeave(this);
    }
}

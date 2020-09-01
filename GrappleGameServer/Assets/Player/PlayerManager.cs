using SharedFiles.Utility;

namespace Player
{
    public class PlayerManager
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
}

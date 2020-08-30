using SharedFiles.Utility;

namespace Player
{
	public class PlayerManager
	{
		public int id;
		public string username;
		public PlayerState state = PlayerState.loadingScreen;
		public Trooper trooper;
		public Spectator spectator;
	}
}
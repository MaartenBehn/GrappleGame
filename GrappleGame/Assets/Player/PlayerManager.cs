using SharedFiles.Utility;

namespace Player
{
	public class PlayerManager
	{
		public int id;
		public string username;
		public PlayerState state = PlayerState.loadingScreen;
		public Trooper.Trooper trooper;
		public Spectator spectator;
	}
}
using UnityEngine;

namespace GameModes
{
	public struct PlayerVote
	{
		public string gameModeName;
		public string lobbyName;
	}
	
	public class Waiting : GameMode
	{
		public static Waiting instance;
		
		public int voteStart = 0;
		public PlayerVote[] playerVotes;

		public override void OnLoad()
		{
			instance = this;
			playerVotes = new PlayerVote[Server.MaxPlayers];
		}
		
		public override void OnUnload()
		{
			
		}

		public override void Update()
		{
			int playerVotes = 0;
		}
	}
}
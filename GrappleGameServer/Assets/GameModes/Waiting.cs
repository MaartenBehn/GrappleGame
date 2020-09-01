using System.Collections.Generic;
using SharedFiles.Utility;
using UnityEngine;

namespace GameModes
{
	public class Waiting : GameMode
	{
		public static Waiting instance;
		
		public int voteStart = 0;
		public List<PlayerVote> playerVotes;

		public override void OnLoad()
		{
			instance = this;
			gameModeType = GameModeType.waiting;
			playerVotes = new List<PlayerVote>();
		}
		
		public override void OnUnload()
		{
			
		}

		public override void Update()
		{
			if (playerVotes.Count > 0 && playerVotes.Count >= GameManager.instance.players.Count)
			{
				//TODO: proper Voting system
				GameManager.instance.ChangeGameState(playerVotes[0].gameModeType, playerVotes[0].lobbyName);
			}
		}
	}
}
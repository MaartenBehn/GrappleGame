using SharedFiles.Utility;

namespace GameModes
{
	public class TeamsGameMode : GameMode
	{
		public static TeamsGameMode instance;
		
		public override void OnLoad()
		{
			instance = this;
			gameModeType = GameModeType.lastManStanding;
		}
		
		public override void OnUnload()
		{
			
		}

		public override void Update()
		{
			
		}
	}
}
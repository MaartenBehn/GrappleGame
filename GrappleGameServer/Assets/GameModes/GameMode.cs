using SharedFiles.Utility;

namespace GameModes
{
	public class GameMode
	{
		public GameModeType gameModeType;
		public virtual void OnLoad() { }
		public virtual void OnUnload() { }
		public virtual void Update() { }
	}
}
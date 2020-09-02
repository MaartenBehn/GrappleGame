using UnityEngine;

namespace UI.Panles
{
	public class TeamsPanel : UIPanel
	{
		public static TeamsPanel instance;
		
		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
			}
			else if (instance != this)
			{
				Debug.Log("Instance already exists, destroying object!");
				Destroy(this);
			}

			panelType = PanelType.teamsPanel;
			usedOverlays = new[]
			{
				OverlayType.healthOverlay
			};
		}

		public override void OnLoad()
		{
			Cursor.lockState = CursorLockMode.Locked;
			base.OnLoad();
		}
	}
}
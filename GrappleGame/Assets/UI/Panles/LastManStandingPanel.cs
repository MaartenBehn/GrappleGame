using UnityEngine;

namespace UI.Panles
{
	public class LastManStandingPanel : UIPanel
	{
		public static LastManStandingPanel instance;
		
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

			panelType = PanelType.lastManStandingPanel;
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
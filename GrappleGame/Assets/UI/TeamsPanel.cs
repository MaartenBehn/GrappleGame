using UnityEngine;

namespace UI
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
		}

		public override void OnLoad()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
	}
}
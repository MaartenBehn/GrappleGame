using UnityEngine;

namespace UI
{
	public class PausePanel : UIPanel
	{
		public static PausePanel instance;
		
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

			panelType = PanelType.pausePanel;
		}

		public override void OnLoad()
		{
			Cursor.lockState = CursorLockMode.None;
		}
	}
}
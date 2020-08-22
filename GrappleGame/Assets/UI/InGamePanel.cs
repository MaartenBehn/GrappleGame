using UnityEngine;

namespace UI
{
	public class InGamePanel : UIPanel
	{
		public static InGamePanel instance;
		
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

			panelType = PanelType.inGamePanel;
		}

		public override void OnLoad()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
	}
}
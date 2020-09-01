using UnityEngine;

namespace UI
{
	public class WaitingPanel : UIPanel
	{
		public static WaitingPanel instance;
		
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

			panelType = PanelType.waitingPanel;
		}

		public override void OnLoad()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
	}
}
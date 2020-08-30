using Server;
using UnityEngine;

namespace UI
{ 
	public class LobbyPanel : UIPanel
	{
		public static LobbyPanel instance;
		
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

			panelType = PanelType.connectingPanel;
		}

		public override void OnLoad()
		{
			Cursor.lockState = CursorLockMode.None;
		}
	}
}
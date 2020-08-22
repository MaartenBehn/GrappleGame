using Server;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class StartPanel : UIPanel
	{
		public static StartPanel instance;
		
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

			panelType = PanelType.startPanel;
		}
		
		public override void OnLoad()
		{
			Cursor.lockState = CursorLockMode.None;
		}

		[SerializeField] public InputField usernameField;
		
		/// <summary>Attempts to connect to the server.</summary>
		public void ConnectToServer()
		{
			UIManager.instance.SwitchPanel(PanelType.inGamePanel);
			usernameField.interactable = false;
			Client.instance.ConnectToServer();

		}
	}
}
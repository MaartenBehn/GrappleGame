using Server;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class StartPanel : MonoBehaviour
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
		}

		
		[SerializeField] public InputField usernameField;
		
		/// <summary>Attempts to connect to the server.</summary>
		public void ConnectToServer()
		{
			UIManager.instance.SwitchPanel(PanelType.inGame);
			usernameField.interactable = false;
			Client.instance.ConnectToServer();

		}
	}
}
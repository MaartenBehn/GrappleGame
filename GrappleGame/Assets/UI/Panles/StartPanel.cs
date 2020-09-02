using Server;
using TMPro;
using UnityEngine;

namespace UI.Panles
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
			usernameField.text = UIManager.gameSettings.playerName;
			UpdateServers();
			UpdateServerList();
			SelectedServerChanged();
			base.OnLoad();
		}

		[SerializeField] public TMP_InputField usernameField;
		[SerializeField] public TMP_InputField passwordField;
		[SerializeField] private TMP_Dropdown serverDropdown;

		public void UpdateServers()
		{
			ClientDatabase.UpdateServers();
		}
		
		public void SelectedServerChanged()
		{
			currentSelectedServer = serverDropdown.value < UIManager.gameSettings.favServers.Count ? 
				UIManager.gameSettings.favServers[serverDropdown.value]
				: Client.instance.serverDatas[serverDropdown.value - UIManager.gameSettings.favServers.Count];
			
			passwordField.text = currentSelectedServer.password;
		}

		private void UpdateServerList()
		{
			serverDropdown.options.Clear();
			
			foreach (ServerData serverData in UIManager.gameSettings.favServers)
			{
				serverDropdown.options.Add(new TMP_Dropdown.OptionData(serverData.name));
			}
			
			if(Client.instance.serverDatas == null) return;

			foreach (ServerData serverData in Client.instance.serverDatas)
			{
				serverDropdown.options.Add(new TMP_Dropdown.OptionData(serverData.name));
			}
			
			currentlyShownOnlineServers = Client.instance.serverDatas.ToArray();
		}

		private ServerData[] currentlyShownOnlineServers;
		public ServerData currentSelectedServer;
		private void Update()
		{
			if (currentlyShownOnlineServers != Client.instance.serverDatas.ToArray())
			{
				UpdateServerList();
			}
		}

		/// <summary>Attempts to connect to the server.</summary>
		public void ConnectToServer()
		{
			UIManager.gameSettings.playerName = usernameField.text;
			currentSelectedServer.password = passwordField.text;
			UIManager.WritSettingsFile();

			Client.instance.ip = currentSelectedServer.ip;
			
			UIManager.instance.SwitchPanel(PanelType.connectingPanel);
			Client.instance.ConnectToServer();
		}
	}
}
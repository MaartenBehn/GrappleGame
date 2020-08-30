using System;
using System.Collections.Generic;
using Server;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

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
			usernameField.text = UIManager.gameSettings.playerName;
			UpdateServers();
		}

		[SerializeField] public TMP_InputField usernameField;
		[SerializeField] private TMP_Dropdown serverDropdown;

		public void UpdateServers()
		{
			ClientDatabase.UpdateServers();
		}

		// TODO: This is fucking retarded but it will always be up to date. Please fix this tis wastes a lot of performance.
		private void Update()
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
		}

		/// <summary>Attempts to connect to the server.</summary>
		public void ConnectToServer()
		{
			Client.instance.ip = serverDropdown.value < UIManager.gameSettings.favServers.Count ? 
					UIManager.gameSettings.favServers[serverDropdown.value].ip
				: Client.instance.serverDatas[serverDropdown.value - UIManager.gameSettings.favServers.Count].ip;
			
			UIManager.instance.SwitchPanel(PanelType.inGamePanel);
			usernameField.interactable = false;
			Client.instance.ConnectToServer();

			UIManager.gameSettings.playerName = usernameField.text;
			
			UIManager.WritSettingsFile();
		}
	}
}
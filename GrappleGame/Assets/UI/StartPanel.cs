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
			UpdateServers();

		}

		[SerializeField] public TMP_InputField usernameField;
		[SerializeField] private TMP_Dropdown serverDropdown;

		public void UpdateServers()
		{
			Database.instance.Request();
		}

		private void Update()
		{
			if(Database.instance.serverDatas == null) return;
			
			serverDropdown.options.Clear();

			foreach (ServerData serverData in Database.instance.serverDatas)
			{
				serverDropdown.options.Add(new TMP_Dropdown.OptionData(serverData.name));
			}
		}

		/// <summary>Attempts to connect to the server.</summary>
		public void ConnectToServer()
		{
			Client.instance.ip = Database.instance.serverDatas[serverDropdown.value].ip;
			
			UIManager.instance.SwitchPanel(PanelType.inGamePanel);
			usernameField.interactable = false;
			Client.instance.ConnectToServer();

		}
	}
}
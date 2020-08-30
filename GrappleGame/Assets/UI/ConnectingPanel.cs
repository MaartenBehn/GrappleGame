using System;
using Server;
using UnityEngine;

namespace UI
{
	public class ConnectingPanel : UIPanel
	{
		public static ConnectingPanel instance;
		
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
			Cursor.lockState = CursorLockMode.Locked;
		}

		float timer = 0.0f;
		[SerializeField] private float serverTimeOutTime; // in sec
		private void Update()
		{
			if (GameManager.players.ContainsKey(Client.instance.myId))
			{
				UIManager.instance.SwitchPanel(PanelType.inGamePanel);
				return;
			}
			
			timer += Time.deltaTime;
			if (!(timer >= serverTimeOutTime)) return;
			
			Client.instance.Disconnect();
			UIManager.instance.SwitchPanel(PanelType.startPanel);
		}
	}
}
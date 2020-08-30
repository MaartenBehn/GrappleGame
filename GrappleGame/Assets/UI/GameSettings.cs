using System;
using System.Collections.Generic;
using System.Numerics;
using Server;
using UnityEngine;

namespace UI
{
	[Serializable]
	public class GameSettings
	{
		public Vector2Int currentResolution = new Vector2Int(1280, 720);
		public bool fullScreen = false;

		public List<ServerData> favServers = new List<ServerData>()
		{
			new ServerData()
			{
				name = "Local Host",
				ip = "127.0.0.1"
			}
		};

		public string playerName = "new player";
	}
	
}
using SharedFiles.Utility;
using UnityEngine;

public static class ServerDatabase
{
	public static void UpdateServer()
	{
		ThreadManager.ExecuteOnMainThread(() =>
		{
			DeleteServer();

			ServerData serverData = new ServerData()
			{
				ip = Server.ip,
				name = ServerManager.instance.name,
				maxPlayers = Server.MaxPlayers,
				players = ServerManager.instance.clinetsInGame.Count,
				version = Application.version
			};
			
			Database.instance.Push(JsonUtility.ToJson(serverData), Database.instance.firebase.Child("Servers").Child(ServerManager.instance.name));
		});
	}
    
	public static  void DeleteServer()
	{
		Database.instance.Delete(Database.instance.firebase.Child("Servers").Child(ServerManager.instance.name));
	}
}
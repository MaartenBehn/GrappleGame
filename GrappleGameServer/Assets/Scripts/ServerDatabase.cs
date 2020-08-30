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
				name = NetworkManager.instance.name,
				maxPlayers = Server.MaxPlayers,
				players = Server.conectedClinets,
				version = Application.version
			};
			
			Database.instance.Push(JsonUtility.ToJson(serverData), Database.instance.firebase.Child("Servers").Child(NetworkManager.instance.name));
		});
	}
    
	public static  void DeleteServer()
	{
		Database.instance.Delete(Database.instance.firebase.Child("Servers").Child(NetworkManager.instance.name));
	}
}
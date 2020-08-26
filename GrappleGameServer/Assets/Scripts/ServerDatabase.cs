using UnityEngine;

public static class ServerDatabase
{
	public static void UpdateServer()
	{
		ThreadManager.ExecuteOnMainThread(() =>
		{
			DeleteServer();
			
			ServerData serverData = new ServerData();
			serverData.ip = Server.ip;
			serverData.name = NetworkManager.instance.name;
			serverData.maxPlayers = Server.MaxPlayers;
			serverData.players = Server.conectedClinets;
			
			Database.instance.Push(JsonUtility.ToJson(serverData), Database.instance.firebase.Child("Servers").Child(NetworkManager.instance.name));
		});
	}
    
	public static  void DeleteServer()
	{
		Database.instance.Delete(Database.instance.firebase.Child("Servers").Child(NetworkManager.instance.name));
	}
}

using Newtonsoft.Json.Linq;
using UnityEngine;
using Utility;

namespace Server
{
	public static class ClientDatabase
	{
		public static void UpdateServers()
        {
	        Database.instance.AddGetAction((json) =>
            {
	            Client.instance.serverDatas.Clear();
	            JObject jo = JObject.Parse(json); 
	            JToken jServers = jo.First;
	            foreach (JToken jServer in jServers)
	            {
		            ServerData serverData = JsonUtility.FromJson<ServerData>(jServer.First.First.ToString());
		            Client.instance.serverDatas.Add(serverData);
	            }
            });
            
            Database.instance.Request(Database.instance.firebase.Child("Servers"));
        }
	}
}
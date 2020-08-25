using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Server;
using SimpleFirebaseUnity;
using UnityEditor;
using UnityEngine;

namespace Utility
{
    public class Database : MonoBehaviour
    {
        public static Database instance;
    
        [SerializeField] private string webAdress;
        public string WebAdress => webAdress;
    
        private Firebase firebase;
        private FirebaseQueue firebaseQueue;

        public List<ServerData> serverDatas;

        private void Awake()
        {
            instance = this;

            firebase = Firebase.CreateNew(webAdress);
            firebaseQueue = new FirebaseQueue(true);
        
            firebase.OnGetSuccess += OnGet;
        }
        
        public void Request()
        {
            firebaseQueue.AddQueueGet(firebase);
        }

        void OnGet(Firebase inputFirebase, DataSnapshot snapshot)
        {
            serverDatas = new List<ServerData>();
            
            JObject jo = JObject.Parse(snapshot.RawJson);
            JToken jServers = jo["Servers"].First;
            foreach (JToken jServer in jServers)
            {
                ServerData server = new ServerData();
                server.ip = (string) jServer.First.First["IP"];
                server.name = (string) jServer.First.First["Name"];
                server.maxPlayers = (int) jServer.First.First["MaxPlayers"];
                server.players = (int) jServer.First.First["Players"];
                
                serverDatas.Add(server);
            }

        }
    }
}

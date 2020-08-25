using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using SimpleFirebaseUnity;
using UnityEditor;
using UnityEngine;

public class Database : MonoBehaviour
{
    public static Database Instance { get; private set; }
    
    public string webAdress;
    
    private Firebase firebase;
    private FirebaseQueue firebaseQueue;

    private void Awake()
    {
        Instance = this;

        firebase = Firebase.CreateNew(webAdress);
        firebaseQueue = new FirebaseQueue(true);
        
        firebase.OnGetSuccess += OnGet;
    }
    
    public void Request(string at)
    {
        firebaseQueue.AddQueueGet(firebase, at);
    }

    void OnGet(Firebase inputFirebase, DataSnapshot snapshot)
    {
        JObject jObject= JObject.Parse(snapshot.RawJson);
    }
    
    public void UpdateServer()
    {
        ThreadManager.ExecuteOnMainThread(() =>
        {
            firebaseQueue.AddQueueDelete(firebase.Child("Servers").Child(NetworkManager.instance.name));
        
            JObject jObject = new JObject();
            jObject.Add("Name", NetworkManager.instance.name);
            jObject.Add("IP", Server.ip);
            jObject.Add("MaxPlayers", Server.MaxPlayers);
            jObject.Add("Players", Server.conectedClinets);
        
            firebaseQueue.AddQueuePush(firebase.Child("Servers").Child(NetworkManager.instance.name),jObject.ToString(), true);
        });
    }
    
    public void DeleteServer()
    {
        firebaseQueue.AddQueueDelete(firebase.Child("Servers").Child(NetworkManager.instance.name));
    }
}
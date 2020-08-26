using System;
using System.Collections.Generic;
using SimpleFirebaseUnity;
using UnityEngine;

public class Database : MonoBehaviour
{
    public static Database instance { get; private set; }
    
    public string webAdress;
    
    public Firebase firebase;
    private FirebaseQueue firebaseQueue;

    private List<Action<String>> onGetFunctions;
    
    private void Awake()
    {
        instance = this;

        firebase = Firebase.CreateNew(webAdress);
        firebaseQueue = new FirebaseQueue(true);
        
        firebase.OnGetSuccess += OnGet;
        onGetFunctions = new List<Action<String>>();
    }

    public void AddGetAction(Action<String> action)
    {
        onGetFunctions.Add(action);
    }
    
    public void Request(Firebase firebasePos)
    {
        firebaseQueue.AddQueueGet(firebasePos);
    }

    void OnGet(Firebase inputFirebase, DataSnapshot snapshot)
    {
        if(onGetFunctions.Count == 0) return;
        onGetFunctions[0](snapshot.RawJson);
        onGetFunctions.RemoveAt(0);
    }

    public void Push(String json, Firebase firebasePos)
    {
        firebaseQueue.AddQueuePush(firebasePos,json, true);
    }

    public void Delete(Firebase firebasePos)
    {
        firebaseQueue.AddQueueDelete(firebasePos);
    }
}
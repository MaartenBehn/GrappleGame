using System;
using System.Collections;
using System.Collections.Generic;
using SharedFiles.Lobby;
using UnityEditor.SceneManagement;
using UnityEngine;

public enum LobbyType
{
    enter,
    respawn,
    locked
}

public class ServerManager : MonoBehaviour
{
    public static ServerManager instance;

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
        
        players = new List<Player>();
    }
    
    public string name;
    public string password;
    public GameObject playerPrefab;
    public List<GameObject> lobbyPreFabList;
    
    [SerializeField] private GameObject startLobby;
    [SerializeField] LobbyType startLobbyType;
    
    [HideInInspector] public LobbyData currentLobbyData;
    [HideInInspector] public string currentLobbyPreFabName;
    public List<Player> players;
    [HideInInspector] public LobbyType lobbyType;
    
    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 50;
        
        Server.Start(50, 17685);

        ServerDatabase.UpdateServer();
        
        LoadLobby(startLobby.name);
    }

    public void LoadLobby(string name)
    {
        if (currentLobbyData != null)
        {
            Destroy(currentLobbyData.gameObject);
        }
        
        GameObject newLobbyPreFab = lobbyPreFabList.Find(gameObject => gameObject.name == name);
        currentLobbyPreFabName = newLobbyPreFab.name;
        currentLobbyData = Instantiate(newLobbyPreFab).GetComponent<LobbyData>();

        foreach (Player player in players)
        {
            ServerSend.LobbyChange(player.client.id);
        }
    }
    
    private void OnApplicationQuit()
    {
        Server.Stop();
        ServerDatabase.DeleteServer();
    }
}

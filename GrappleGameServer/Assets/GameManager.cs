using System;
using System.Collections;
using System.Collections.Generic;
using GameModes;
using SharedFiles.Lobby;
using SharedFiles.Utility;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.PlayerLoop;



public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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

    [HideInInspector] public LobbyData currentLobbyData;
    [HideInInspector] public string currentLobbyPreFabName;
    public List<Player> players;

    public GameMode currentGameMode;
    
    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 50;
        
        Server.Start(50, 17685);

        ServerDatabase.UpdateServer();

        currentGameMode = new Waiting();
    }

    private void Update()
    {
        
    }

    public void ChangeGameMode(GameMode gameMode)
    {
        
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

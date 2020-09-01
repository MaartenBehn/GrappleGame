using System;
using System.Collections;
using System.Collections.Generic;
using GameModes;
using Player;
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
        
        players = new List<Player.PlayerManager>();
        
        
    }
    
    public string name;
    public string password;
    public GameObject playerPrefab;
    public List<GameObject> lobbyPreFabList;
    
    [SerializeField] private GameObject startLobby;

    [HideInInspector] public LobbyData currentLobbyData;
    [HideInInspector] public string currentLobbyPreFabName;
    public List<Player.PlayerManager> players;

    public GameMode currentGameMode;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 50;
        
        Server.Start(50, 17685);

        ServerDatabase.UpdateServer();

        ChangeGameState(GameModeType.waiting, startLobby.name);
    }

    private void Update()
    {
        
    }

    public void ChangeGameState(GameModeType gameModeType, string lobbyName)
    {
        foreach (PlayerManager player in players)
        {
            ChangePlayerState(player, PlayerState.loadingScreen);
        }
        
        if (currentGameMode != null)
        {
            currentGameMode.OnUnload();
        }

        switch (gameModeType)
        {
            case GameModeType.waiting:
                currentGameMode = new Waiting();
                break;
        }
        
        if (currentLobbyData != null)
        {
            Destroy(currentLobbyData.gameObject);
        }
        
        GameObject newLobbyPreFab = lobbyPreFabList.Find(gameObject => gameObject.name == lobbyName);
        currentLobbyPreFabName = newLobbyPreFab.name;
        currentLobbyData = Instantiate(newLobbyPreFab).GetComponent<LobbyData>();

        foreach (Player.PlayerManager player in players)
        {
            ServerSend.GameStateChange(player.client.id);
        }
        
        currentGameMode.OnLoad();
        
    }

    public void ChangePlayerState(PlayerManager player, PlayerState state)
    {
        if(player.state == state) return;
        player.state = state;

        switch (state)
        {
            case PlayerState.loadingScreen:
                if (player.trooper != null)
                {
                    Destroy(player.trooper.gameObject);
                }
                break;
            case PlayerState.inGame:
                player.trooper = Instantiate(playerPrefab).GetComponent<Trooper>();
                player.trooper.player = player;
                break;
            case PlayerState.spectator:
                if (player.trooper != null)
                {
                    Destroy(player.trooper.gameObject);
                }
                break;
        }
        
        ServerSend.PlayerState(player);
    }

    private void OnApplicationQuit()
    {
        Server.Stop();
        ServerDatabase.DeleteServer();
    }
}

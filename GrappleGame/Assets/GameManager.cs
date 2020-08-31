using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Player.Trooper;
using Server;
using SharedFiles.Utility;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    public GameObject localTrooperPrefab;
    public GameObject trooperPrefab;

    public new GameObject camera;
    
    public List<GameObject> lobbyPreFabList;
    public GameObject currentLobby;

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
    }

    public void LobbyChange(String name)
    {
        if (currentLobby != null)
        {
            Destroy(currentLobby);
        }
        
        GameObject newLobbyPreFab = lobbyPreFabList.Find(gameObject => gameObject.name == name);
        currentLobby = Instantiate(newLobbyPreFab);
    }
    
    public void PlayerEnter(int id, string username)
    {
        players.Add(id, new PlayerManager()
        {
            id = id,
            username = username      
        });
    }
    
    public void PlayerStateChanged(int id, PlayerState state)
    {
        PlayerManager player = players[id];
        
        if(player.state == state) return;
        player.state = state;

        switch (state)
        {
            case PlayerState.loadingScreen:
                if (player.trooper != null)
                {
                    Destroy(player.trooper.gameObject);
                }
                if (player.spectator != null)
                {
                    Destroy(player.spectator.gameObject);
                }
                break;
            case PlayerState.inGame:
                if (player.spectator != null)
                {
                    Destroy(player.spectator.gameObject);
                }

                player.trooper = id == Client.instance.myId ? 
                    Instantiate(localTrooperPrefab).GetComponent<Trooper>() : 
                    Instantiate(trooperPrefab).GetComponent<Trooper>();
                break;
            case PlayerState.spectator:
                if (player.trooper != null)
                {
                    Destroy(player.trooper.gameObject);
                }
                break;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

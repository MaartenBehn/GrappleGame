using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Server;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;

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

    public void QuitGame()
    {
        Application.Quit();
    }
}

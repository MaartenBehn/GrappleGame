using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

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
        
        clinetsInGame = new List<Client>();
    }

    public string name;
    public string password;
    public GameObject playerPrefab;
    public List<GameObject> lobbyPreFabList;
    [SerializeField] private GameObject startLobby;
    public GameObject currentLobby;
    public string currentLobbyPreFabName;

    public List<Client> clinetsInGame;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 50;
        
        Server.Start(50, 17685);

        ServerDatabase.UpdateServer();
        
        LoadLobby(startLobby.name);
    }

    private void OnApplicationQuit()
    {
        Server.Stop();
        ServerDatabase.DeleteServer();
    }
    
    public void LoadLobby(string name)
    {
        if (currentLobby != null)
        {
            Destroy(currentLobby);
        }
        
        GameObject newLobbyPreFab = lobbyPreFabList.Find(gameObject => gameObject.name == name);
        currentLobbyPreFabName = newLobbyPreFab.name;
        currentLobby = Instantiate(newLobbyPreFab);

        foreach (Client client in clinetsInGame)
        {
            ServerSend.LobbyChange(client.id);
        }
    }

    public Player InstantiatePlayer()
    {
        return Instantiate(playerPrefab, new Vector3(0f, 0.5f, 0f), Quaternion.identity).GetComponent<Player>();
    }
}

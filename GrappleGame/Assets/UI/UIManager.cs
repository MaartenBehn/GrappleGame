using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Server;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public enum PanelType
    {
        startPanel = 0,
        inGamePanel = 1,
        pausePanel = 2,
        settingsPanel = 3
    }

    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
        public static GameSettings gameSettings;

        List<UIPanel> panelList;
        UIPanel lastActivePanel;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                gameSettings = new GameSettings();
            }
            else if (instance != this)
            {
                Debug.Log("Instance already exists, destroying object!");
                Destroy(this);
            }
        }

        private void Start()
        {
            panelList = GetComponentsInChildren<UIPanel>().ToList();
            panelList.ForEach(x => x.gameObject.SetActive(false));
            SwitchPanel(PanelType.startPanel);
            
            ReadFile();
        }

        private void Update()
        {
            switch (lastActivePanel.panelType)
            {
                case PanelType.inGamePanel:
                    if (Input.GetKeyDown(KeyCode.Escape)) { SwitchPanel(PanelType.pausePanel); }
                    break;
                case PanelType.pausePanel:
                    if (Input.GetKeyDown(KeyCode.Escape)) { SwitchPanel(PanelType.inGamePanel); }
                    break;
                case PanelType.settingsPanel:
                    if (Input.GetKeyDown(KeyCode.Escape)) { SwitchPanel(PanelType.inGamePanel); }
                    break;
            }
        }

        public void SwitchPanel(PanelType type)
        {
            if (lastActivePanel != null)
            {
                lastActivePanel.OnUnload();
                lastActivePanel.gameObject.SetActive(false);
            }

            UIPanel desiredCanvas = panelList.Find(x => x.panelType == type);
            if (desiredCanvas != null)
            {
                desiredCanvas.gameObject.SetActive(true);
                lastActivePanel = desiredCanvas;
                lastActivePanel.OnLoad();
            }
            else { Debug.LogWarning("The desired canvas was not found!"); }
        }

        public void SwitchPanel(int typeId)
        {
            SwitchPanel((PanelType) typeId);
        }
        
        public void WritFile()
        {
            File.WriteAllText(Application.dataPath + "/settings.json",JsonUtility.ToJson(gameSettings));
        }

        public void ReadFile()
        {
            if (File.Exists(Application.dataPath + "/settings.json"))
            {
                gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.dataPath + "/settings.json"));
                return;
            }
            WritFile();
        }
    }
}

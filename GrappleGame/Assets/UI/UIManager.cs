using System;
using System.Collections.Generic;
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
        settingsPanel = 4
    }

    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        List<UIPanel> panelList;
        UIPanel lastActivePanel;

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

        private void Start()
        {
            panelList = GetComponentsInChildren<UIPanel>().ToList();
            panelList.ForEach(x => x.gameObject.SetActive(false));
            SwitchPanel(PanelType.startPanel);
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
    }
}

using System.Collections.Generic;
using System.Linq;
using Server;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public enum PanelType
    {
        startMenu,
        inGame,
        pausePanel
    }

    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        List<UIPanelType> panelList;
        UIPanelType lastActivePanel;

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
            
            panelList = GetComponentsInChildren<UIPanelType>().ToList();
            panelList.ForEach(x => x.gameObject.SetActive(false));
            SwitchPanel(PanelType.startMenu);
        }
        
        public void SwitchPanel(PanelType type)
        {
            if (lastActivePanel != null)
            {
                lastActivePanel.gameObject.SetActive(false);
            }

            UIPanelType desiredCanvas = panelList.Find(x => x.panelType == type);
            if (desiredCanvas != null)
            {
                desiredCanvas.gameObject.SetActive(true);
                lastActivePanel = desiredCanvas;
            }
            else { Debug.LogWarning("The desired canvas was not found!"); }
        }
    }
}

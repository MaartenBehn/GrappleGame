using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace SharedFiles.Lobby
{
    public class LobbyData : MonoBehaviour
    {
        public static LobbyData instance;
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

            // proper ids
            for (int i = 0; i < snappingObjects.Length; i++)
            {
                snappingObjects[i].name = "snap " + i;
            }
        }

        public Image preViewImage;
        public GameObject[] snappingObjects;
    }
}

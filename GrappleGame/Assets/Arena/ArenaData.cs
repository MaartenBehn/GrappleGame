using UnityEngine;

namespace Arena
{
    public class ArenaData : MonoBehaviour
    {
        [SerializeField] public GameObject[] snappingObjects;
        public static ArenaData instance;
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
    }
}

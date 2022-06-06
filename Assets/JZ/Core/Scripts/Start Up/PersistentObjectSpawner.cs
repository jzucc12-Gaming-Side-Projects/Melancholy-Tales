using UnityEngine;

namespace JZ.CORE
{
    //Originally from GameDev.TV//
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab = null;
        static bool spawned = false;

        void Awake()
        {
            if(spawned) return;
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
            spawned = true;
        }
    }
}

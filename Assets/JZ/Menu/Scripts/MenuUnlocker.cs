using UnityEngine;

namespace JZ.MENU
{
    public class MenuUnlocker : MonoBehaviour
    {
        [SerializeField] MenuManager menu;
        [SerializeField] string key;
        [SerializeField] int valueIfNotOpened = 0;
        [SerializeField] int valueIfOpened = 1;
        [SerializeField] bool reset = false;

        private void Start() 
        {
            if(reset)
                PlayerPrefs.DeleteKey(key);

            int value = PlayerPrefs.GetInt(key, valueIfNotOpened);
            if(value == valueIfOpened)
            {
                menu.ShutDown();
            }
            else
            {
                menu.StartUp();    
                PlayerPrefs.SetInt(key, valueIfOpened);
            }

            if(reset)
                PlayerPrefs.DeleteKey(key);
        }
    }
}
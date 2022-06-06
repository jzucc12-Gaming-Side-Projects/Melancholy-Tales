using UnityEngine;

namespace JZ.CORE
{
    public class DevModeCheck : MonoBehaviour
    {
        [SerializeField] bool matchDev = true;

        private void Awake() 
        {
            gameObject.SetActive(!(DevMode.inDevMode ^ matchDev));    
        }
    }
}
using JZ.INPUT;
using UnityEngine;
using UnityEngine.UI;

namespace JZ.UI
{
    public class ControlsScreenUI : MonoBehaviour
    {
        #region //Cached Components
        [SerializeField] Button toKeyboardButton = null;
        [SerializeField] Button toControllerButton = null;
        [SerializeField] GameObject keyboardScreen = null;
        [SerializeField] GameObject controllerScreen = null;
        MenuingInputSystem menuSystem;
        #endregion


        #region //Monobehaviour
        private void Awake() 
        {
            menuSystem = new MenuingInputSystem(new GeneralInputs());    
        }

        private void OnEnable() 
        {
            SwapScreen(false);
            menuSystem.Activate(true);
            GetComponent<Canvas>().enabled = true;
        }

        private void OnDisable() 
        {
            menuSystem.Activate(false);
            GetComponent<Canvas>().enabled = false;
        }

        private void Update()
        {
            if(menuSystem.xNav > 0)
            {
                menuSystem.ExpendXDir();
                SwapScreen(true);
            }
            else if(menuSystem.xNav < 0)
            {
                menuSystem.ExpendXDir();
                SwapScreen(false);
            }
        }
        #endregion

        public void SwapScreen(bool _toController)
        {
            controllerScreen.SetActive(_toController);
            keyboardScreen.SetActive(!_toController);
            toControllerButton.gameObject.SetActive(!_toController);
            toKeyboardButton.gameObject.SetActive(_toController);
        }
    }
}
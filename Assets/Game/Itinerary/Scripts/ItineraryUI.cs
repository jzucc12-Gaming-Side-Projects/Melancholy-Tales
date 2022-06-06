using CFR.ITINERARY;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CFR.UI
{
    public class ItineraryUI : MonoBehaviour
    {
       private Itinerary itinerary;
       private ShipStateDisplayManager shipStateDisplay;

        #region //UI Components
        [Header("UI Components")]
        [SerializeField] private GameObject takeOffUIContainer = null;
        [SerializeField] private TextMeshProUGUI takeOffUIHeader = null;
        [SerializeField] private TextMeshProUGUI takeOffUICounter = null;
        [SerializeField] private Image previousArrow = null;
        [SerializeField] private Image nextArrow = null;
        #endregion


        #region //Monobehaviour
        private void Awake()
        {
            itinerary = GetComponent<Itinerary>();
            shipStateDisplay = FindObjectOfType<ShipStateDisplayManager>();
        }

        private void OnEnable() 
        {
            itinerary.StateChanged += UpdateUI; 
        }

        private void OnDisable() 
        {
            itinerary.StateChanged -= UpdateUI; 
        }
        #endregion

        #region //UI Methods
        //Public
        public void ShowUI()
        {
            takeOffUIContainer.SetActive(true);
            foreach(var display in FindObjectsOfType<LZItineraryDisplay>())
                display.Show(true);
        }
        
        public void HideUI()
        {
            takeOffUIContainer.SetActive(false);
            nextArrow.enabled = false;
            previousArrow.enabled = false;
            foreach(var display in FindObjectsOfType<LZItineraryDisplay>())
                display.Show(false);
        }

        //Private 
        private void UpdateUI(int _index)
        {
            if(_index == 0)
            {
                takeOffUIHeader.text = "Initial";
                takeOffUICounter.text = "State";
                previousArrow.enabled = false;
                nextArrow.enabled = true;
                shipStateDisplay.ItineraryUpdate(_index, false);
            }
            else if(_index == itinerary.GetHeldValue() + 1)
            {
                takeOffUIHeader.text = "Current";
                takeOffUICounter.text = "State";
                previousArrow.enabled = true;
                nextArrow.enabled = false;
                shipStateDisplay.ItineraryUpdate(_index, true);
            }
            else
            {
                takeOffUIHeader.text = "Take-Off";
                takeOffUICounter.text = "#" + _index;
                previousArrow.enabled = true;
                nextArrow.enabled = true;
                shipStateDisplay.ItineraryUpdate(_index, false);
            }
        }
        #endregion
    }
}

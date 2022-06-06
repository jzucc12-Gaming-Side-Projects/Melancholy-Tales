using CFR.INPUT;
using CFR.ITINERARY;
using UnityEngine;
using UnityEngine.UI;

namespace CFR.STAGE
{
    public class TutorialBox : MonoBehaviour
    {
        static int numberOpen = 0;
        Canvas myCanvas;
        PlayerInputManager playerInputManager;
        ItineraryInputManager itineraryInputs;

        private void Awake() 
        {
            myCanvas = GetComponent<Canvas>();    
            playerInputManager = FindObjectOfType<PlayerInputManager>();
            itineraryInputs = FindObjectOfType<ItineraryInputManager>();
        }

        private void OnEnable() 
        {
            numberOpen++;
        }

        private void OnDisable() 
        {
            numberOpen = Mathf.Max(0, numberOpen - 1);
        }

        private void Update() 
        {
            if(!myCanvas.enabled)
                enabled = false;

            if(numberOpen == 0)
            {
                playerInputManager.LockInputs(false);
                if(itineraryInputs != null) itineraryInputs.LockInputs(false);
            }
            else
            {
                playerInputManager.LockInputs(true);
                if(itineraryInputs != null) itineraryInputs.LockInputs(true);
            }
        }
    }
}
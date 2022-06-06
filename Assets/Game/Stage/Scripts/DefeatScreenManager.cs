using System;
using System.Collections.Generic;
using CFR.CARGO;
using CFR.INPUT;
using CFR.ITINERARY;
using CFR.LZ;
using JZ.AUDIO;
using JZ.MENU;
using UnityEngine;
using UnityEngine.UI;

namespace CFR.STAGE
{
    public class DefeatScreenManager : MonoBehaviour
    {
        #region//Manifest defeat variables
        [Header("Manifest Defeat")]
        [SerializeField] GameObject conflictBox = null;
        [SerializeField] Image attackerImage = null;
        [SerializeField] Image loserImage = null;
        [SerializeField] Image attackerTypeImage = null;
        [SerializeField] Image loserTypeImage = null;
        List<LandingZone> losingLZs = new List<LandingZone>();
        #endregion

        #region//Security defeat variables
        [Header("Security Defeat")]
        [SerializeField] GameObject securityBox = null;
        [SerializeField] Image caughtImage = null;
        #endregion

        ItineraryInputManager itineraryInputs;
        Canvas myCanvas;
        public event Action Defeated;
        bool defeated = false;


        #region//Monobehaviour
        private void Awake()
        {
            myCanvas = GetComponent<Canvas>();
            itineraryInputs = FindObjectOfType<ItineraryInputManager>();
        }

        private void Start() 
        {
            conflictBox.SetActive(false);
            securityBox.SetActive(false);
        }

        private void OnEnable()
        {
            Manifest.manifestConflict += DefeatFromManifest;
            SecurityBeam.alarmTripped += DefeatFromSecurity;
            Itinerary.Rewind += Rewound;
        }

        private void OnDisable()
        {
            Manifest.manifestConflict -= DefeatFromManifest;
            SecurityBeam.alarmTripped -= DefeatFromSecurity;
            Itinerary.Rewind -= Rewound;
        }
        #endregion

        #region //Defeat types
        void DefeatFromManifest(Manifest _manifest, CargoItem _attackerItem, CargoItem _loserItem)
        {
            Defeated?.Invoke();
            defeated = true;
            losingLZs.Add(_manifest.GetComponent<LandingZone>());
            GetComponent<AudioManager>().Play("Manifest Fail");
            conflictBox.SetActive(true);
            attackerImage.sprite = _attackerItem.GetItemImage();
            loserImage.sprite = _loserItem.GetItemImage();
            attackerTypeImage.sprite = _attackerItem.GetAttackerType().GetIcon();
            loserTypeImage.sprite = _loserItem.GetLoserType().GetIcon();
        }

        void DefeatFromSecurity(CargoItem _item)
        {
            Defeated?.Invoke();
            defeated = true;
            GetComponent<AudioManager>().Play("Security Alert");
            securityBox.SetActive(true);
            caughtImage.sprite = _item.GetItemImage();
        }
        #endregion

        #region //Interaction with Itinerary
        public void ActivateItinerary()
        {
            itineraryInputs.SetDefeatState(true);
            itineraryInputs.ActivateToggle();
            itineraryInputs.ActivateItinerary(true);
            GetComponent<MenuManager>().ShutDown(false);
            foreach(var lz in losingLZs)
                lz.SetAnimationActive(false);

            foreach(var beam in FindObjectsOfType<SecurityBeam>())
                beam.SetTrippedBeam(false);
        }

        public void ItineraryOff()
        {   
            if(!defeated) return;
            FindObjectOfType<ActiveLZController>().ShutDown();
            GetComponent<MenuManager>().StartUp();
            FindObjectOfType<ItineraryInputManager>().DeactivateToggle();
            foreach(var lz in losingLZs)
                lz.SetAnimationActive(true);

            foreach(var beam in FindObjectsOfType<SecurityBeam>())
                beam.SetTrippedBeam(true);
        }

        void Rewound(bool _fromDefeat)
        {
            if(!_fromDefeat) return;
            Start();
            GetComponent<MenuManager>().ShutDown(false);

            defeated = false;
            losingLZs = new List<LandingZone>();

            foreach(var beam in FindObjectsOfType<SecurityBeam>())
                beam.ResetTripped();
        }
        #endregion
    }
}

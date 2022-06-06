using CFR.INPUT;
using UnityEngine;
using CFR.CARGO;
using System;
using JZ.AUDIO;

namespace CFR.LZ
{
    public class ActiveLZController : MonoBehaviour, INavigationController
    {
        #region//Cached variables
        public Manifest myManifest { get; private set; }
        public ManifestNavigator navigator { get; private set; }
        PlayerInputManager inputManager;
        public LandingZone myLZ { get; private set; }
        #endregion

        #region//LZ variables
        public bool isActive { get; private set; }
        public event Action<bool> LZControllerStateChange;
        bool isLocked = false;
        public bool openForTransfer = false;
        #endregion

        public void SetActive(bool _active)
        {
            isActive = _active;
        }

        #region//Monobehaviour
        private void Awake()
        {
            isActive = false;
            inputManager = FindObjectOfType<PlayerInputManager>();
            myManifest = GetComponent<Manifest>();
        }

        private void Start()
        {
            int shipManifestSize = GameObject.FindGameObjectWithTag("ShipManifest").GetComponent<Manifest>().GetMaxSize();
            navigator = new ManifestNavigator(inputManager, false, 1, shipManifestSize);
            ShutDown();
        }

        private void Update()
        {
            navigator.IncreaseFrameBuffer();
            if (!isActive) 
                return;

            navigator.Navigation();

            if(!openForTransfer)
            {
                if(inputManager.menuSystem.selected)
                {
                    GetComponent<AudioManager>().Play("Swap Fail");
                    inputManager.menuSystem.ExpendMenuSelect();
                    GetComponent<Animation>().Play();
                }
            }
            else
                navigator.Selection();
        }
        #endregion

        #region//Startup and shutdown
        public void StartUp(LandingZone _lz)
        {
            myLZ = _lz;
            if(isLocked) return;
            navigator.ResetItem();
            navigator.ResetArrow();
            myManifest.CopyManifest(myLZ.GetManifest());
            navigator.SetSize(myManifest.GetMaxSize());
            LZControllerStateChange?.Invoke(true);
        }

        public void ShutDown()
        {
            if(isLocked) return;
            navigator.ResetItem();
            navigator.ResetArrow();
            isActive = false;
            LZControllerStateChange?.Invoke(false);
        }

        public void ForceOff()
        {
            Lock(false);
            ShutDown();
            Lock(true);
        }

        public void Lock(bool _lock)
        {
            isLocked = _lock;
        }
        
        public void ShipLand(bool _landing)
        {
            navigator.ResetBuffer();
            openForTransfer = _landing;
            if(!_landing)
            {
                navigator.ResetItem();
            }
        }
        #endregion

        #region//Navigation Controller
        public ManifestNavigator GetNavigator()
        {
            return navigator;
        }
        #endregion

        #region //Interaction with Itinerary
        public void DeactivateFromItineraryOpens()
        {
            Lock(false);
            ShutDown();
            Lock(true);
        }

        public void RevertWhenItineraryCloses()
        {
            Lock(false);
            if(myLZ != null)
                StartUp(myLZ);
        }
        #endregion
    }
}

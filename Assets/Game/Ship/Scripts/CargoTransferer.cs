using CFR.CARGO;
using CFR.INPUT;
using JZ.AUDIO;
using System;
using UnityEngine;
using CFR.LZ;

namespace CFR.SHIP
{
    public class CargoTransferer : MonoBehaviour, INavigationController
    {
        #region//Cached Variables
        PlayerInputManager inputManager;
        ActiveLZController lzController;
        Manifest myManifest;
        Manifest lzManifest;
        ManifestNavigator shipNavigator;
        ManifestNavigator lzNavigator;
        public static event Action OnSwap;
        #endregion


        #region//Monobehaviour
        void Awake()
        {
            inputManager = FindObjectOfType<PlayerInputManager>();
            lzController = FindObjectOfType<ActiveLZController>();
            myManifest = GetComponent<Manifest>();
        }

        void Start()
        {
            shipNavigator = new ManifestNavigator(inputManager, true, myManifest.GetMaxSize());
            lzManifest = lzController.myManifest;
            lzNavigator = lzController.navigator;
        }

        void Update()
        {
            inputManager.shipSystem.IncreaseBuffer();
            if (!lzController.isActive)
            {
                if (myManifest.GetMaxSize() < 2)
                {
                    shipNavigator.Shutdown();
                    return;
                }
                else if (shipNavigator.arrow < 0)
                    shipNavigator.StartUp();

                shipNavigator.Navigation();
                shipNavigator.Selection();
                shipNavigator.IncreaseFrameBuffer();
            }
            else
            {
                shipNavigator.Shutdown();
                if (!lzController.openForTransfer)
                    return;
            }

            TransferCargo();
        }


        #endregion

        #region //Swapping
        private void TransferCargo()
        {
            if (inputManager.shipSystem.transferInput)
            {
                inputManager.shipSystem.ExpendTransferInput();
                if (lzController.isActive)
                {
                    Manifest firstManifest = lzManifest;
                    if (lzNavigator.activeItem >= lzManifest.GetMaxSize())
                        firstManifest = myManifest;

                    Manifest secondManifest = lzManifest;
                    if (lzNavigator.arrow >= lzManifest.GetMaxSize())
                        secondManifest = myManifest;

                    Swap(lzNavigator, firstManifest, secondManifest);
                }
                else
                    Swap(shipNavigator, myManifest, myManifest);
            }
        }

        void Swap(ManifestNavigator _navigator, Manifest _firstManifest, Manifest _secondManifest)
        {
            if(!_navigator.activeItemSet) return;
            if(_navigator.arrow == _navigator.activeItem) return;

            int firstItemNo = _navigator.activeItem;
            int secondItemNo = _navigator.arrow;

            if(lzController.isActive)
            {
                if(firstItemNo >= lzManifest.GetMaxSize())
                    firstItemNo -= lzManifest.GetMaxSize();

                if(secondItemNo >= lzManifest.GetMaxSize()) 
                    secondItemNo -= lzManifest.GetMaxSize();
            }

            CargoItem firstItem = _firstManifest.GetItem(firstItemNo);
            CargoItem secondItem = _secondManifest.GetItem(secondItemNo);

            if(!_firstManifest.TrySwap(secondItem, firstItemNo)) 
            {
                GetComponent<AudioManager>().Play("Swap Fail");
                return;
            }
            if(!_secondManifest.TrySwap(firstItem, secondItemNo)) 
            {
                GetComponent<AudioManager>().Play("Swap Fail");
                return;
            }

            _firstManifest.SwapItems(firstItemNo, secondItemNo, _secondManifest);
            _navigator.ResetItem();
            GetComponent<AudioManager>().Play("Swap Success");
            OnSwap?.Invoke();
        }
        #endregion

        public ManifestNavigator GetNavigator()
        {
            return shipNavigator;
        }
    }
}

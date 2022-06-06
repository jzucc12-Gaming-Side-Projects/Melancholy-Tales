using CFR.CARGO;
using CFR.LZ;
using CFR.SHIP;
using UnityEngine;
using UnityEngine.UI;

namespace CFR.UI
{
    public class SneakHelpUI : MonoBehaviour
    {
        #region //Display Info
        [Header("Display Info")]
        ActiveLZController lZController;
        Manifest myManifest;
        Color activeQuirkColor = Color.white;
        Color inactiveQuirkColor = Color.gray;
        int numCaught = 0;
        int numSneaky = 0;

        #endregion

        #region //UI Components
        [Header("UI Components")]
        [SerializeField] Transform resultContainer = null;
        [SerializeField] Image sneakyImage = null;
        #endregion


        #region //Monobehaviour
        private void Awake()
        {
            myManifest = GetComponentInParent<Manifest>();
            lZController = FindObjectOfType<ActiveLZController>();
        }

        private void OnEnable()
        {
            RefreshUI();
            CargoTransferer.OnSwap += RefreshUI;
            lZController.LZControllerStateChange += Activated;
        }

        private void OnDisable()
        {
            CargoTransferer.OnSwap -= RefreshUI;
            lZController.LZControllerStateChange -= Activated;
        }
        #endregion

        #region //Refresh UI
        private void Activated(bool _)
        {
            RefreshUI();
        }

        private void RefreshUI()
        {
            if(myManifest == null) return;

            CountSneaky();
            UpdateResult();
        }

        private void CountSneaky()
        {
            numCaught = 0;
            numSneaky = 0;

            for(int ii = 0; ii < myManifest.GetMaxSize(); ii++)
            {
                var item = myManifest.GetItem(ii);
                if(item == null) continue;
                if(!item.IsSneaky()) continue;
                numSneaky++;
                if(myManifest.GetHoldType(ii) == HoldType.conceal) continue;
                if(myManifest.GetHoldType(ii) == HoldType.SC) continue;
                numCaught++;
            }
        }

        private void UpdateResult()
        {
            bool useCheck = numCaught < 2 && numSneaky > 0;
            bool useX = numCaught > 1 && numSneaky > 0;
            bool useNA = numSneaky == 0;

            
            Color imageColor = useCheck ? activeQuirkColor : inactiveQuirkColor;
            sneakyImage.color = imageColor;
            resultContainer.GetChild(0).gameObject.SetActive(useCheck);
            resultContainer.GetChild(1).gameObject.SetActive(useX);
            resultContainer.GetChild(2).gameObject.SetActive(useNA);
        }

        private void SetCheckOrX(Transform _container, bool _useCheck, bool _useNA = false)
        {
            _container.GetChild(0).gameObject.SetActive(_useCheck);
            _container.GetChild(1).gameObject.SetActive(!_useCheck && !_useNA);
            _container.GetChild(2).gameObject.SetActive(!_useCheck && _useNA);
        }
        #endregion
    }
}
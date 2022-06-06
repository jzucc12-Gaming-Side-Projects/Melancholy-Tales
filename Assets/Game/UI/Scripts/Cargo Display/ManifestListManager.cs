using CFR.INPUT;
using UnityEngine;
using CFR.CARGO;
using CFR.SHIP;

namespace CFR.UI
{
    public class ManifestListManager : MonoBehaviour
    {
        #region//Cached variabled
        PlayerInputManager inputManager;
        Manifest myManifest;
        Manifest shipManifest;
        ManifestListEntry[] entries;
        #endregion

        #region//Other variables
        bool controllerKeys;
        [SerializeField] bool isShipList = false;
        [SerializeField] bool showHoldType = true;
        #endregion

        #region//Monobehaviour
        void Awake()
        {
            inputManager = FindObjectOfType<PlayerInputManager>();
            entries = GetComponentsInChildren<ManifestListEntry>();
            shipManifest = GameObject.FindGameObjectWithTag("ShipManifest").GetComponent<Manifest>();
            myManifest = GetComponentInParent<Manifest>();
        }
        
        void Start()
        {
            controllerKeys = GameSettings.isUsingGamepad;
            if (myManifest) UpdateList();
        }

        private void OnEnable()
        {
            CargoTransferer.OnSwap += UpdateList;
            if(isShipList) myManifest.ItineraryChanged += UpdateList;
        }

        private void OnDisable()
        {
            CargoTransferer.OnSwap -= UpdateList;
            if(isShipList) myManifest.ItineraryChanged -= UpdateList;
        }

        private void Update()
        {
            if (controllerKeys != GameSettings.isUsingGamepad)
            {
                controllerKeys = GameSettings.isUsingGamepad;
                UpdateList();
            }
        }
        #endregion


        #region//List updating
        public void UpdateList()
        {
            int maxSize = (isShipList ? Globals.maxShipHold : Globals.maxLZHold);
            for (int ii = 0; ii < maxSize; ii++)
            {
                if (ii < myManifest.GetMaxSize())
                    entries[ii].UpdateEntry(myManifest.GetItem(ii), myManifest.GetHoldType(ii), showHoldType);
                else
                    entries[ii].gameObject.SetActive(false);
            }

            if(isShipList) return;

            for(int ii = maxSize; ii < maxSize + Globals.maxShipHold; ii++)
            {
                if (ii < maxSize + shipManifest.GetMaxSize())
                    entries[ii].UpdateEntry(shipManifest.GetItem(ii - maxSize), shipManifest.GetHoldType(ii - maxSize), showHoldType);
                else
                    entries[ii].gameObject.SetActive(false);
            }
        }

        public void SetManifest(Manifest _manifest)
        {
            myManifest = _manifest;
            UpdateList();
        }

        public void SetArrow(int _itemNo, bool _enable)
        {
            if(_itemNo > myManifest.GetMaxSize() - 1)
                _itemNo += Globals.maxLZHold - myManifest.GetMaxSize();

            entries[_itemNo].SetArrow(_enable);
        }

        public void HighlightListItem(int _itemNo, bool _isFirst)
        {
            if(_itemNo > myManifest.GetMaxSize() - 1)
                _itemNo += Globals.maxLZHold - myManifest.GetMaxSize();

            Color color = (_isFirst ? GameSettings.color1 : GameSettings.color1);
            entries[_itemNo].ChangeHighlightColor(color);
        }

        public void UnhighlightListItem(int _itemNo)
        {
            if(_itemNo > myManifest.GetMaxSize() - 1)
                _itemNo += Globals.maxLZHold - myManifest.GetMaxSize();

            entries[_itemNo].ChangeHighlightColor(GameSettings.defaultColor);
        }

        void DisplayChange(bool _)
        {
            UpdateList();
        }
        #endregion

        #region//Get keys
        //string GetKey(int _number)
        //{
        //    if (GameSettings.isUsingGamepad)
        //        return GetKeyFromGamepad(_number);
        //    else
        //        return GetKeyFromKeyboard(_number);
        //}

        //string GetKeyFromKeyboard(int _number)
        //{
        //    string keyLong = actionList.bindings[_number].path;
        //    return keyLong.Substring(keyLong.Length - 1).ToUpper();
        //}

        //string GetKeyFromGamepad(int _number)
        //{
        //    string output;
        //    string first = (_number % 2 == 0 ? "L" : "R");
        //    string mod = (Mathf.RoundToInt(_number / 2) % 2 == 0 ? "T" : "B");

        //    int half = (isShipList ? Globals.maxShipHold : Globals.maxLZHold) / 2;
        //    if (_number < half)
        //        output = first + mod;
        //    else
        //    {
        //        output = "D";
        //        if (isShipList) _number++;

        //        switch(_number)
        //        {
        //            case 4:
        //                output += "L";
        //                break;
        //            case 5:
        //                output += "U";
        //                break;
        //            case 6:
        //                output += "R";
        //                break;
        //            case 7:
        //                output += "D";
        //                break;
        //        }
        //    }

        //    switch(GameSettings.CurrentGamepad())
        //    {
        //        case GamepadType.sony:
        //            output = output.Replace("T", "2");
        //            output = output.Replace("B", "1");
        //            break;
        //        case GamepadType.nSwitch:
        //            output = output.Replace("LT", "ZL");
        //            output = output.Replace("RT", "ZR");
        //            output = output.Replace("B", "");
        //            break;
        //    }

        //    return output;
        //}
        #endregion
    }
}
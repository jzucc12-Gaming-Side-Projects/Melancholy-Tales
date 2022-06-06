using UnityEngine;

namespace CFR.CARGO
{
    [CreateAssetMenu(fileName = "Base Quirk", menuName = "Cargo/Quirk/Base", order = 0)]
    public class Quirk : ScriptableObject 
    {
        enum CheckType
        {
            Swap = 0,
            Takeoff = 1,
            Manifest = 2,
            Security = 3,
            Landing = 4,
            Other = 5
        }
        [SerializeField] CheckType myCheckType = CheckType.Manifest;
        [SerializeField] Sprite myIcon = null;

        #region //Determine Check Types
        public bool IsSwapCheck() { return myCheckType == CheckType.Swap; }
        public bool IsTakeOffCheck() { return myCheckType == CheckType.Takeoff; }
        public bool IsManifestCheck() { return myCheckType == CheckType.Manifest; }
        public bool IsSecurityCheck() { return myCheckType == CheckType.Security; }
        public bool IsLandingCheck() { return myCheckType == CheckType.Landing; }
        #endregion

        public Sprite GetIcon() { return myIcon; }

        protected bool ObservantCheck(CargoItem _attackingItem, Manifest _manifest)
        {
            if(!_attackingItem.IsSneaky() && _manifest.HasQuirk("Observant"))
                return true;
            else return false;
        }

        //Possibly use later for other quirks
        // bool Other(Manifest _manifest, int _itemNo, bool _offensiveCheck)
        // {
        //     if(_manifest.GetItemsInManifest() == 1) return true;

        //     Offensive
        //     if(ObservantCheck(_manifest.GetItem(_itemNo), _manifest)) return false;
        //     HoldType myHold = _manifest.GetHoldType(_itemNo);
        //     return myHold == HoldType.conceal || myHold == HoldType.SC;

        //     Lawful
        //     bool observant = _manifest.GetItem(_itemNo).HasQuirk("Observant");
        //     return _manifest.HasQuirk("Illegal", !observant);
        // }    
    }


}
using System.Collections.Generic;
using UnityEngine;

namespace CFR.CARGO
{
    [CreateAssetMenu(fileName = "Cargo", menuName = "Cargo/New Cargo Item", order = 1)]
    public class CargoItem : ScriptableObject
    {
        #region//Identification Variables
        [Header("Identification")]
        [SerializeField] string displayName = null;
        [SerializeField] Sprite itemImage = null;

        [Header("Item Specifics")]
        [SerializeField] AttackerType attackerType = null;
        [SerializeField] LoserType loserType = null;
        [SerializeField] List<Quirk> myQuirks = new List<Quirk>();
        #endregion


        #region//Getters
        public string GetName() 
        {
            if(string.IsNullOrEmpty(displayName))
                return name;
            else
                return displayName; 
        }
        public Sprite GetItemImage() { return itemImage; }
        public AttackerType GetAttackerType() { return attackerType; }
        public LoserType GetLoserType() { return loserType; }
        public IEnumerable<Quirk> GetQuirks() { return myQuirks; }
        #endregion

        #region //Type and Quirk checking
        public bool HasAttackerType(AttackerType _type) { return attackerType == _type; }
        public bool HasLoserType(LoserType _type) { return loserType == _type; }
        public bool HasQuirk(Quirk _quirk) { return myQuirks.Contains(_quirk); }
        public bool HasQuirk(string _quirkName) { return myQuirks.Find(x => x.name == _quirkName); }
        public bool IsSneaky() { return HasQuirk("Sneaky"); }
        public bool IsObservant() { return HasQuirk("Observant"); }
        #endregion

        #region//Defeat checking
        public bool DefeatCheck(CargoItem _testingItem, bool _observantPresent)
        {
            if(_observantPresent && !IsSneaky()) return false;

            var attacker = GetAttackerType();
            if(!attacker) return false;

            var loser = attacker.GetDefeats();
            if(!loser) return false;

            return _testingItem.HasLoserType(loser);
        }
        #endregion
    }
}
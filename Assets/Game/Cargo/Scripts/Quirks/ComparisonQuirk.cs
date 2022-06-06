using UnityEngine;

namespace CFR.CARGO
{
    [CreateAssetMenu(fileName = "Comparison Quirk", menuName = "Cargo/Quirk/Comparison", order = 1)]
    public class ComparisonQuirk : Quirk
    {
        [SerializeField] ComparisonType myType = ComparisonType.needMoreLosers;


        //Return true if defeat is cancelled
        public bool ComparisonCheck(CargoItem _attackingItem, Manifest _manifest)
        {
            if(ObservantCheck(_attackingItem, _manifest)) return true;

            int loserCount = _manifest.GetTypeCount(_attackingItem.GetAttackerType().GetDefeats());
            int attackerCount = 1;
            if(_attackingItem.GetAttackerType().name != "Renegade") 
                attackerCount = _manifest.GetTypeCount(_attackingItem.GetAttackerType());
            else
                loserCount--;

            if(myType == ComparisonType.needMultipleLosers) return loserCount > 1;
            else if(myType == ComparisonType.needLessLosers) return loserCount < attackerCount;
            else return loserCount > attackerCount;
        }
    }

    enum ComparisonType
    {
        needMoreLosers = 0,
        needLessLosers = 1,
        needMultipleLosers = 2
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CFR.CARGO
{
    [CreateAssetMenu(fileName = "Base Quirk", menuName = "Cargo/Quirk/Hold", order = 2)]
    public class HoldQuirk : Quirk
    {
        [SerializeField] List<HoldType> requiredTypes;

        //Return true on item being in an acceptable hold
        public bool HoldCheck(HoldType _holdType)
        {
            return requiredTypes.Contains(_holdType);
        }
    }
}

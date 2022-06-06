using UnityEngine;

namespace CFR.CARGO
{
    [CreateAssetMenu(fileName = "Cargo", menuName = "Cargo/Type/Attacker Type", order = 0)]
    public class AttackerType : CargoType
    {
        [Tooltip("Cargo type this serves as an 'attacker' to")] [SerializeField] LoserType defeats = null;
        public LoserType GetDefeats() { return defeats; }
    }
}
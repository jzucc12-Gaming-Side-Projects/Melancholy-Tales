using UnityEngine;

namespace CFR.CARGO
{
    public abstract class CargoType : ScriptableObject
    {
        [SerializeField] Sprite myIcon = null;
        public Sprite GetIcon() { return myIcon; }
    }
}
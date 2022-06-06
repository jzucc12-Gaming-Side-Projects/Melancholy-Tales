using UnityEngine;

namespace CFR.CARGO
{
    public class TimerQuirk : Quirk
    {
        [SerializeField] int maxCount;

        public int GetMaxCount() { return maxCount; }

        public bool Timer(ref int _currCount, bool _reset)
        {
            if(_reset)
            {
                _currCount = maxCount;
                return true;
            }

            _currCount--;
            return _currCount > 0;
        }
    }
}

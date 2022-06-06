using CFR.CARGO;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace CFR.LZ
{
    public class LandingZone : MonoBehaviour
    {
        #region//cached variables
        Manifest myManifset = null;
        [SerializeField] Animator movementAnimator;
        [SerializeField] Animator defeatAnimation;
        ActiveLZController lzController;
        [SerializeField] UnityEvent shipLanded;
        [SerializeField] UnityEvent shipTakeOff;
        #endregion

        #region//LZ variables
        [SerializeField] LZType myLZType = LZType.mid;
        public bool isStart => myLZType == LZType.starting;
        public bool isMid => myLZType == LZType.mid;
        public bool isGoal => myLZType == LZType.goal;
        #endregion

        public static event Action Victory;


        #region//Monobehaviour
        private void Awake()
        {
            lzController = FindObjectOfType<ActiveLZController>();
            myManifset = GetComponent<Manifest>();
        }
        #endregion

        #region//Landing/TakeOff
        public void Landing(LandingZone _currentLZ)
        {
            bool isHere = _currentLZ == this;
            movementAnimator.enabled = false;

            if (!isHere) 
            {
                bool defeat = myManifset.ManifestCheck();
                if(!defeat) return; 
                defeatAnimation.enabled = true;
            }
            else
            {
                lzController.ShipLand(true);
                shipLanded?.Invoke();
            } 
        }

        public void TakingOff(LandingZone _currentLZ)
        {
            movementAnimator.enabled = true;
            if (_currentLZ != this) return;
            lzController.ShipLand(false);

            shipTakeOff?.Invoke();
            if (isGoal && myManifset.IsManifestFull())
                Victory?.Invoke();
        }
        #endregion

        #region//Getters and Setters
        public Manifest GetManifest() { return myManifset; }
        public void SetAnimationActive(bool _setAnim) 
        { 
            defeatAnimation.enabled = _setAnim; 
            defeatAnimation.GetComponent<SpriteRenderer>().enabled = _setAnim;
        }
        #endregion
    }

    enum LZType
    {
        starting = 0,
        mid = 1,
        goal = 2
    }
}
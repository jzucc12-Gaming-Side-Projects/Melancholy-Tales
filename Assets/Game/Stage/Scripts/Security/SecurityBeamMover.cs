using System.Collections;
using System.Collections.Generic;
using CFR.INPUT;
using CFR.ITINERARY;
using CFR.LZ;
using CFR.SHIP;
using TMPro;
using UnityEngine;

namespace CFR.STAGE
{
    public class SecurityBeamMover : MonoBehaviour, IItineraryElement
    {
        #region //Components
        [Header("Components")]
        [SerializeField] TextMeshProUGUI movingText = null;
        [SerializeField] BoxCollider2D[] laserSources = new BoxCollider2D[2];
        PlayerInputManager playerInputs;
        ItineraryInputManager itineraryInputs;
        Canvas lzDisplayCanvas;
        #endregion

        #region //Movement Variables
        [Header("Movement Variables")]
        [SerializeField] int maxTimer = 2;
        [SerializeField] Vector3 endPosition;
        Vector3 startPosition;
        [SerializeField] float moveSpeed = .075f;
        int currentTimer = 0;
        #endregion

        #region //Saved states
        MovingLaserState heldState = new MovingLaserState();
        List<MovingLaserState> savedStates = new List<MovingLaserState>();
        StageManager stageManager;
        #endregion

        
        #region //Monobehaviour
        private void Awake() 
        {
            stageManager = FindObjectOfType<StageManager>();
            lzDisplayCanvas = FindObjectOfType<ActiveLZController>().GetComponent<Canvas>();
            playerInputs = FindObjectOfType<PlayerInputManager>();
            itineraryInputs = FindObjectOfType<ItineraryInputManager>();
            startPosition = transform.localPosition;
            ResetTimer();
        }

        private void OnEnable() 
        {
            ShipMoveSM.stateChanged += ShipLandedCheck;
        }

        private void OnDisable() 
        {
            ShipMoveSM.stateChanged -= ShipLandedCheck;
        }
        #endregion

        #region //Moving/Timer 
        void ShipLandedCheck(ShipMoveState _newState)
        {
            if(_newState is LandingState)
                DecrementTimer();
        }

        void DecrementTimer()
        {
            currentTimer--;
            movingText.text = currentTimer.ToString();
            if(currentTimer > 0) return;

            StartCoroutine(MoveLaser());
        }

        void ResetTimer()
        {
            currentTimer = maxTimer;
            movingText.text = maxTimer.ToString();
        }

        IEnumerator MoveLaser()
        {
            lzDisplayCanvas.enabled = false;
            playerInputs.LockInputs(true);
            itineraryInputs.DeactivateToggle();
            foreach(var source in laserSources)
                source.enabled = false;

            Vector3 target = (transform.localPosition == startPosition ? endPosition : startPosition);
            while(transform.localPosition != target)
            {
                Vector3 newPosition = Vector3.MoveTowards(transform.localPosition, target, moveSpeed);
                transform.localPosition = newPosition;
                yield return new WaitForFixedUpdate();
            }

            if(!stageManager.ended)
            {
                playerInputs.LockInputs(false);
                itineraryInputs.ActivateToggle();
                lzDisplayCanvas.enabled = true;
            }

            ResetTimer();
            foreach(var source in laserSources)
                source.enabled = true;
        }
        #endregion

        #region //IItinerary Element
        public void SaveState()
        {
            var newState = new MovingLaserState();
            newState.laserPosition = transform.localPosition;
            newState.timerValue = currentTimer;
            savedStates.Add(newState);
        }

        public void HoldCurrentState()
        {
            heldState.laserPosition = transform.localPosition;
            heldState.timerValue = currentTimer;
            savedStates.Add(heldState);
        }

        public void RestoreCurrentState()
        {
            transform.localPosition = heldState.laserPosition;
            currentTimer = heldState.timerValue;
            savedStates.RemoveAt(savedStates.Count - 1);
            heldState = new MovingLaserState();
            movingText.text = currentTimer.ToString();
        }

        public void ShowState(int _index)
        {
            var showedState = savedStates[_index];
            transform.localPosition = showedState.laserPosition;
            currentTimer = showedState.timerValue;
            movingText.text = currentTimer.ToString();
        }

        public void DeleteOldStates(int _limit)
        {
            savedStates.RemoveRange(_limit + 1, savedStates.Count - _limit - 2);
        }

        public void RevertHeldState(int _index)
        {
            heldState = new MovingLaserState(savedStates[_index]);
            foreach(var source in laserSources)
                source.enabled = true;
        }

        private struct MovingLaserState
        {
            public Vector3 laserPosition;
            public int timerValue;

            public MovingLaserState(MovingLaserState _copyState)
            {
                laserPosition = _copyState.laserPosition;
                timerValue = _copyState.timerValue;
            }
        }
        #endregion
    }
}

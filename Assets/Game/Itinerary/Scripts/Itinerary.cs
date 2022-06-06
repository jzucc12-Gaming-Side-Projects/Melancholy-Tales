using System;
using System.Collections.Generic;
using System.Linq;
using JZ.MENU;
using UnityEngine;
using CFR.SHIP;
using CFR.INPUT;
using JZ.AUDIO;

namespace CFR.ITINERARY
{
    public class Itinerary : MonoBehaviour, IItineraryElement
    {
        public static event Action<bool> Rewind;
        [SerializeField] private MenuManager popUpWindow = null;
        private PlayerInputManager playerInputs;
        private ItineraryInputManager inputManager;
        private AudioManager sfxPlayer;
        public event Action<int> StateChanged;
        private int takeOffs = -1;
        private int heldTakeOffs = 0;

        #region //Monobehaviour
        private void Awake() 
        {
            inputManager = GetComponent<ItineraryInputManager>();    
            playerInputs = FindObjectOfType<PlayerInputManager>();
            sfxPlayer = GetComponent<AudioManager>();
        }

        private void OnEnable() 
        {
            ShipMoveSM.stateChanged += Save;
        }

        private void OnDisable()
        {
            ShipMoveSM.stateChanged -= Save;
        }

        private void Update() 
        {
            if(inputManager.GetItineraryActive())
                playerInputs.LockInputs(true);

            if(inputManager.GetDidMove())
            {
                int newIndex = Mathf.Clamp(takeOffs + inputManager.GetMove(), 0, heldTakeOffs + 1);
                if(takeOffs != newIndex)
                    sfxPlayer.Play("Menu Move");

                inputManager.ExpendMove();
                UpdateState(newIndex);
            }

            if(inputManager.GetSelected())
            {
                inputManager.ExpendSelect();
                StateSelected();
            }
        }
        #endregion

        #region//Activation
        public void Activate()
        {
            if(!inputManager.GetDefeatState())
                playerInputs.LockInputs(true);
            sfxPlayer.Play("Activate");

            foreach(var element in GetElements())
                element.HoldCurrentState();

            UpdateState(takeOffs);    
        }

        public void Deactivate()
        {
            if(!inputManager.GetDefeatState())
                playerInputs.LockInputs(false);
    
            popUpWindow.ShutDown();
            if(!sfxPlayer.IsPlaying("Rewind"))
                sfxPlayer.Play("Deactivate");

            foreach(var element in GetElements())
                element.RestoreCurrentState();
        }
        #endregion

        #region //Game State Methods
        //Public
        public void RevertState()
        {
            sfxPlayer.Play("Rewind");
            foreach(var element in GetElements())
            {
                element.RevertHeldState(takeOffs);
                element.DeleteOldStates(takeOffs);
            }

            Rewind?.Invoke(inputManager.GetDefeatState());
            inputManager.SetDefeatState(false);
            inputManager.ActivateItinerary(false);
        }

        //Private
        private void StateSelected()
        {
            if(takeOffs == heldTakeOffs + 1)
            {
                inputManager.ActivateItinerary(false);
            }
            else
            {
                sfxPlayer.Play("Menu Select");
                inputManager.DeactivateUsage();
                popUpWindow.StartUp();
            }
        }

        private void RestoreState()
        {
            foreach(var element in GetElements())
                element.RestoreCurrentState();
        }

        private void UpdateState(int _index)
        {
            foreach(var element in GetElements())
                element.ShowState(_index);

            StateChanged?.Invoke(_index);
        }

        private void Save(ShipMoveState _newState)
        {
            if(inputManager.GetItineraryActive()) return;
            if(_newState is MovingState)
            {
                foreach(var element in GetElements())
                    element.SaveState();
            }
        }
        #endregion

        #region //Getters
        private IEnumerable<IItineraryElement> GetElements()
        {
            return FindObjectsOfType<MonoBehaviour>().OfType<IItineraryElement>();
        }

        public int GetHeldValue() { return heldTakeOffs; }
        #endregion

        #region //IItinerary Element
        public void SaveState()
        {
            takeOffs++;
        }

        public void HoldCurrentState()
        {
            heldTakeOffs = takeOffs;
            takeOffs++;
        }

        public void RestoreCurrentState()
        {
            takeOffs = heldTakeOffs;
            heldTakeOffs = 0;
        }

        public void ShowState(int _index)
        {
            takeOffs = _index;
        }

        public void DeleteOldStates(int _limit)
        {
            takeOffs = _limit;
        }

        public void RevertHeldState(int _index)
        {
            heldTakeOffs = _index;
        }
        #endregion
    }
}
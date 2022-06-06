using System;
using System.Collections;
using UnityEngine;

namespace JZ.CORE
{
    public abstract class MyStateMachine<T> : MonoBehaviour where T : State 
    {
        #region//State variables
        public T currentState { get; protected set; }
        public T previousState { get; protected set; }
        public bool lockState = false;
        public static event Action<T> stateChanged;
        #endregion


        #region//Monobehaviour
        protected virtual void Start()
        {
            StateStartUp();
        }    

        protected virtual void Update()
        {
            currentState.StateUpdate();
        }
        #endregion

        #region//Start up
        protected abstract void StateStartUp();
        #endregion

        #region//State Logic
        //Methods return true if state successfully changed
        public bool ChangeState(T _newState)
        {
            bool startUp = currentState == null;

            if (!startUp && DontChangeState(_newState)) return false;

            if (!startUp)
            {
                currentState.EndState(_newState);
                previousState = currentState;
            }
            currentState = _newState;
            currentState.StartState();
            stateChanged?.Invoke(_newState);
            return true;
        }

        //Returns true if state should not change
        protected virtual bool DontChangeState(T _newState)
        {
            if (_newState == currentState) return true;
            if (lockState) return true;
            return false;
        }
        #endregion

        #region//Other
        public void RunRoutine(IEnumerator _CR)
        {
            StartCoroutine(_CR);
        }
        #endregion
    }
}
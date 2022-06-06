using UnityEngine.InputSystem;
using UnityEngine;
using System;
using JZ.MENU;
using JZ.CORE;

namespace CFR.INPUT
{
    public class StageInputManager : MonoBehaviour
    {
        #region//Ship Input system
        [SerializeField] MenuManager[] subMenus = null;
        [SerializeField] Canvas rulesGlossary = null;
        SceneTransitionManager transitionManager;
        GeneralInputs generalInputs;
        InputAction pauseAction;
        InputAction menuSchemeAction;
        #endregion

        #region//Inputs
        bool isPaused;
        #endregion

        #region//Events variables
        public static event Action<bool> OnPause;
        #endregion


        #region//Monobehaviour
        private void Awake()
        {
            generalInputs = new GeneralInputs();
        }

        private void Start()
        {
            transitionManager = FindObjectOfType<SceneTransitionManager>();
        }

        private void OnEnable()
        {
            StartUp();
            OnPause += Paused;
        }

        private void OnDisable()
        {
            ShutDown();
            OnPause -= Paused;
        }
        #endregion

        #region//Start up/shutdown
        void StartUp()
        {
            InitializeInputs();
            InitializeActions();
            SubscribeEvents(true);
            EnableActions(true);
        }
        void ShutDown()
        {
            SubscribeEvents(false);
            EnableActions(false);
        }
        void InitializeActions()
        {
            pauseAction = generalInputs.Options.Pause;
        }
        void InitializeInputs()
        {
            isPaused = false;
        }
        void SubscribeEvents(bool _startUp)
        {
            SubscribePauseOptions(_startUp);
        }
        void EnableActions(bool _startUp)
        {
            EnablePauseOptions(_startUp);
        }

        public void EndGameState(bool _ending)
        {
            EnableActions(!_ending);
            SubscribeEvents(!_ending);
        }
        #endregion

        #region//Pause/Options
        void SubscribePauseOptions(bool _enable)
        {
            if(_enable)
            {
                pauseAction.started += PauseInput;
            }
            else
            {
                pauseAction.started -= PauseInput;
            }
        }

        void EnablePauseOptions(bool _enable)
        {
            if(_enable)
            {
                pauseAction.Enable();
            }
            else
            {
                pauseAction.Disable();
            }
        }

        void PauseInput(InputAction.CallbackContext context)
        {
            if(transitionManager.IsTransitioning()) return;
            if(rulesGlossary.enabled) return;
            foreach(var menu in subMenus)
                if(menu.enabled) return;

            OnPause?.Invoke(!isPaused);
        }

        public void ManualPause(bool _pause)
        {
            OnPause?.Invoke(_pause);
        }

        void Paused(bool _paused)
        {
            isPaused = _paused;
        }
        #endregion
    }
}
using UnityEngine;
using JZ.DIALOGUE;
using GameDevTV.Assets.Dialogues;
using JZ.INPUT;
using JZ.CORE;
using JZ.BUTTON;
using UnityEngine.UI;

namespace JZ.CUTSCENE
{
    public class CutsceneManager : MonoBehaviour, IProgressible
    {
        [SerializeField] HoldButton skipButton = null;
        [SerializeField] bool hasText = true;
        Graphic continueUI = null;
        PlayerConversant playerConversant = null;
        CutsceneInputSystem inputSystem;
        TextPrinter printer;
        float holdTimeToSkip;


        private void Awake() 
        {
            printer = FindObjectOfType<TextPrinter>();
            playerConversant = FindObjectOfType<PlayerConversant>();
            inputSystem = new CutsceneInputSystem(new CutsceneInputs());
        }

        private void Start() 
        {
            holdTimeToSkip = skipButton.GetHoldTimer();
        }

        private void OnEnable() 
        {
            inputSystem.Activate(true);
        }

        private void OnDisable() 
        {
            inputSystem.Activate(false);
        }

        private void Update()
        {
            SkipSceneCheck();
            if(!hasText) return;
            if(FindObjectOfType<SceneTransitionManager>().IsTransitioning())
            {
                printer.Pause(true);
                return;
            }
            else if(!printer.isPrinting)
                printer.Pause(false);

            NextLineCheck();
            if(continueUI) continueUI.enabled = !printer.isPrinting;
        }

        void NextLineCheck()
        {
            if(!playerConversant) return;
            if(!playerConversant.IsActive()) return;
            if(inputSystem.nextLineInput)
            {
                if(printer.isPrinting)
                    printer.ForceStop();
                else if(playerConversant.HasNext())
                    playerConversant.Next();
                else
                    FindObjectOfType<HoldButton>().onClick?.Invoke();
                    
                inputSystem.ExpendNextLineInput();
            }
        }

        void SkipSceneCheck()
        {
            if(inputSystem.GetIsHolding())
            {
                float currTime = Time.realtimeSinceStartup - inputSystem.startHoldTime;
                if(currTime >= holdTimeToSkip)
                {
                    skipButton.onClick?.Invoke();
                    inputSystem.StopSkipSceneHold();
                }
            }
        }

        public float GetProgressPercentage()
        {
            if(inputSystem.GetIsHolding())
                return (Time.realtimeSinceStartup - inputSystem.startHoldTime) / holdTimeToSkip;
            else
                return 0;
        }

        public void SetContinueGraphic(Graphic _graphic)
        {
            if(continueUI) continueUI.enabled = false;
            continueUI = _graphic;
        }
    }
}
using System.Collections;
using JZ.AUDIO;
using JZ.CORE;
using TMPro;
using UnityEngine;

namespace  JZ.DIALOGUE
{
    [RequireComponent(typeof(AudioManager))]
    public class TextPrinter : MonoBehaviour
    {
        #region //Cached variables
        [SerializeField] TextMeshProUGUI myText;
        AudioManager sfxManager;
        AudioClip printSound;
        IEnumerator printRoutine;
        #endregion

        #region //Print variables
        [SerializeField] [Tooltip("Characters displayed per second")] int charPerSecond = 10;
        [SerializeField] [Tooltip("Keep unchecked for allowing text to print in edit mode")] bool inEditMode = true;
        public bool isPrinting;
        string finalText;
        string printSFX = "Text Print";
        #endregion


        #region //Monobheaviour
        private void Awake() 
        {
            sfxManager = GetComponent<AudioManager>();
            inEditMode = false;
        }
        #endregion

        #region //Getters
        public string GetFinalText() { return finalText; }
        #endregion

        #region //Character printing
        public void PrintNewText(string _newText)
        {   
            if(string.IsNullOrEmpty(_newText)) return;
            if(isPrinting) ForceStop();

            finalText = _newText;
            myText.text = "";
            printRoutine = PrintRoutine();
            StartCoroutine(printRoutine);
        }

        void ReprintText()
        {
            printRoutine = PrintRoutine();
            StartCoroutine(printRoutine);
        }

        IEnumerator PrintRoutine()
        {
            isPrinting = true;
            int frameNo = 0;
            int charPauseFrames = Mathf.Max(Application.targetFrameRate / charPerSecond, 1);

            int charCount = 1;
            myText.text = finalText.Substring(0,charCount);

            if(!inEditMode) sfxManager.Play(printSFX);

            while(myText.text != finalText)
            {
                frameNo++;
                yield return null;
                if (frameNo % charPauseFrames != 0) continue;
                charCount++;
                charCount = CheckForMarkup(charCount);
                string currentDisplay = finalText.Substring(0, charCount);
                myText.text = currentDisplay;
            }

            if (!inEditMode) sfxManager.Stop(printSFX);
            printRoutine = null;
            isPrinting = false;
        }

        public void Pause(bool _pause)
        {
            if(_pause)
            {
                StopAllCoroutines();
                if(!inEditMode) sfxManager.Stop(printSFX);
                isPrinting = false;
            }
            else if(printRoutine != null)
                StartCoroutine(printRoutine);
        }

        public void ForceStop(bool _stopInPlace = false)
        {
            if(!inEditMode) sfxManager.Stop(printSFX);
            if(!_stopInPlace) myText.text = finalText;
            StopAllCoroutines();
            printRoutine = null;
            isPrinting = false;
        }
        
        private int CheckForMarkup(int _charCount)
        {
            if(_charCount > finalText.Length - 1) return _charCount;

            int start = 2;
            int searchDepth = 30;

            if (finalText[_charCount] == '<')
            {
                for (int ii = start; ii < searchDepth; ii++)
                {
                    if(_charCount + ii > finalText.Length - 1) return _charCount;
                    if (finalText[_charCount + ii] != '>') continue;
                    return CheckForMarkup(_charCount + ii + 1);
                }
            }

            return _charCount;
        }
        #endregion
    }
}

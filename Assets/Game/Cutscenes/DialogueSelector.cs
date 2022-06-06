using System.Collections;
using GameDevTV.Assets.Dialogues;
using JZ.CORE;
using JZ.DIALOGUE;
using UnityEngine;

namespace CFR.CUTSCENE
{
    public class DialogueSelector : MonoBehaviour
    {
        [SerializeField] bool isIntro = false;
        bool wait = true;
        bool starting = false;

        private void Update() 
        {
            if(starting) return;
            if(!wait)
            {
                starting = true;
                StartCoroutine(StartDialogue());
                return;
            }

            wait = FindObjectOfType<SceneTransitionManager>().IsTransitioning();
        }

        IEnumerator StartDialogue()
        {
            yield return new WaitForSeconds(1);
            int logNo = PlayerPrefs.GetInt(Globals.lastLog);
            string suffixText = isIntro ? "Intro" : "Outro";
            Dialogue myDialogue = Resources.Load<Dialogue>("Cutscenes/Log " + logNo + " " + suffixText);
            FindObjectOfType<PlayerConversant>().StartDialogue(FindObjectOfType<AIConversant>(), myDialogue);
            enabled = false;
        }
    }
}
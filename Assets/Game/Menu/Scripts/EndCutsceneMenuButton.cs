using JZ.CORE;
using JZ.MENU;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CFR.MENU
{
    public class EndCutsceneMenuButton : MenuMemberComponent
    {
        [SerializeField] GameObject nextButtonContainer = null;
        [SerializeField] TextMeshProUGUI stageHeader = null;
        bool started = false;
        
        private void Start() 
        {
            transform.parent.SetAsLastSibling();
            nextButtonContainer.transform.SetAsLastSibling();
            started = true;

            bool inLog10 = PlayerPrefs.GetInt(Globals.lastLog) == 10;
            GetComponentInParent<MenuManager>().RemakeMembers(inLog10);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            GetComponent<Button>().onClick.AddListener(GoToCutscene);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            GetComponent<Button>().onClick.RemoveListener(GoToCutscene);
        }

        protected override void OnHover(bool _isHovering) 
        { 
            if(!_isHovering || !started) return;
            stageHeader.text = "The End";
        }

        void GoToCutscene()
        {
            FindObjectOfType<SceneTransitionManager>().TransitionToScene("End Cutscene", AnimType.fade);
        }
    }
}

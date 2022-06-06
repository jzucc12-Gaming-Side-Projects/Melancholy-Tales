using System;
using CFR.STAGE;
using JZ.CORE;
using JZ.MENU;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CFR.MENU
{
    public class StageSelectButton : MenuMemberComponent
    {
        [SerializeField] StageInfo stage = null;
        [SerializeField] TextMeshProUGUI stageID;

        protected override void Awake() 
        {
            base.Awake();
            if(stage) stageID.text = stage.GetStageID();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            GetComponent<Button>().onClick.AddListener(GoToStage);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            GetComponent<Button>().onClick.RemoveListener(GoToStage);
        }

        protected override void OnHover(bool _isHovering) { }

        void GoToStage()
        {
            FindObjectOfType<SceneTransitionManager>().TransitionToScene(stage.GetStageID(), AnimType.ship);
        }

        public void AddStage(StageInfo _stage) 
        { 
            stage = _stage; 
            stageID.text = stage.GetStageID();
        }

        public StageInfo GetStage() { return stage; }
    }
}
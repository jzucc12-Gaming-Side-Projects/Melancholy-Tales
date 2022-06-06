using CFR.STAGE;
using JZ.MENU;
using JZ.BUTTON;
using TMPro;
using UnityEngine;

namespace CFR.MENU
{
    public class LogMenu : ScrollingMenu
    {
        [Header("Log Specific")]
        [SerializeField] LogInfo myLog = null;
        [SerializeField] GameObject stageButtonPrefab = null;
        [SerializeField] GameObject cutsceneButtonPrefab = null;
        [SerializeField] TextMeshProUGUI logHeader = null;
        [SerializeField] TextMeshProUGUI stageHeader = null;
        int myLogNum => myLog.GetLogNumber();
        bool cancelSkipAfterActivate = false;



        #region //Monobehaviour
        protected override void Awake()
        {
            cantProgressForward = !DevMode.inDevMode && !myLog.HasCompleted();
            CutsceneButtonSetUp(true);
            StageButtonSetUp();
            nextButton.transform.parent.SetAsLastSibling();
            nextButton.GetComponent<LogPrerequisite>().AddLog(myLog);
            base.Awake();
        }

        protected override void Update()
        {
            if(cancelSkipAfterActivate)
            {
                menuSystem.ExpendSkip();
                cancelSkipAfterActivate = false;
            }

            base.Update();
            if(menuSystem.skip > 0)
            {
                NextMenu();
                menuSystem.ExpendSkip();
            }
            else if(menuSystem.skip < 0)
            {
                PreviousMenu();
                menuSystem.ExpendSkip();
            }
        }

        protected override void Start()
        {
            base.Start();
            int lastLog = PlayerPrefs.GetInt(Globals.lastLog);
            if(lastLog == myLogNum)
            {
                scroller.SetPosition(myLogNum - 1);
                int lastStage = PlayerPrefs.GetInt(Globals.lastStage);
                Debug.Log(lastStage);
                SetPosition(lastStage);
            }
            else
            {
                if(lastLog > myLogNum)
                    SetPosition(members.Count - 1, false);
                LockControl(true);
            }
        }
        #endregion

        #region //Overrides
        protected override void MemberHovered(MenuMember _member)
        {
            base.MemberHovered(_member);
            string stageName = "";
            if(_member.GetComponent<StageSelectButton>())
            {
                StageInfo stage = _member.GetComponent<StageSelectButton>().GetStage();
                stageName = stage.GetStageName();
                PlayerPrefs.SetInt(Globals.lastStage, stage.GetStageNo());
            }
            else
            {
                bool isIntro = members.FindIndex(x => x == _member) == 0;
                if(isIntro) 
                {
                    stageName = myLog.GetIntroCutsceneName();
                    PlayerPrefs.SetInt(Globals.lastStage, 0);
                }
                else 
                {
                    PlayerPrefs.SetInt(Globals.lastStage, myLog.GetStageCount()+1);
                }
            }

            if(!string.IsNullOrEmpty(stageName))
                StageHeaderUpdate(stageName);
        }

        protected override void ActivateMenu()
        {
            base.ActivateMenu();

            PlayerPrefs.SetInt(Globals.lastLog,myLogNum);
            StageHeaderUpdate("");
            cancelSkipAfterActivate = true;
        }
        
        protected override void NextMenu()
        {
            base.NextMenu();
            if(enabled) return;

            var nextLogMenu = (LogMenu)nextMenu;
            nextLogMenu.SetPosition(0, true);
        }

        protected override void PreviousMenu()
        {
            base.PreviousMenu();
            if(enabled) return;

            var previousLogMenu = (LogMenu)previousMenu;
            previousLogMenu.SetPosition(previousLogMenu.members.Count - 1, true);
        }
        #endregion

        #region //Set up
        void StageHeaderUpdate(string _stageName)
        {
            string logHeading = "Log " + myLog.GetLogNumber() + ": " + myLog.GetLogName();
            logHeader.text = logHeading;
            stageHeader.text = _stageName;
        }

        void StageButtonSetUp()
        {
            StageInfo lastStage = null;
            foreach(StageInfo stage in myLog.GetStages())
            {
                GameObject stageButton = Instantiate(stageButtonPrefab, transform);
                MenuMember newMember = stageButton.GetComponentInChildren<MenuMember>();
                
                if(!newMember.GetComponent<StageSelectButton>()) return;
                newMember.GetComponent<StageSelectButton>().AddStage(stage);
                members.Add(newMember);

                if(lastStage)
                {
                    newMember.enabled = DevMode.inDevMode || lastStage.HasCompleted();
                    newMember.GetComponent<StagePrerequisite>().AddStage(lastStage);
                }

                lastStage = stage;
            }
        }

        void CutsceneButtonSetUp(bool _isIntro)
        {
            GameObject cutsceneButton = Instantiate(cutsceneButtonPrefab, transform);
            MenuMember newMember = cutsceneButton.GetComponentInChildren<MenuMember>();
            members.Add(newMember);

            if(_isIntro) return;
            newMember.GetComponent<LogPrerequisite>().AddLog(myLog);
            newMember.GetComponentInChildren<TextMeshProUGUI>().text = "Outro";
            newMember.GetComponent<SceneChangeButton>().SetSceneName("Log Outro Scene");
        }
        #endregion
    }
}

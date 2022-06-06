using JZ.BUTTON;
using UnityEngine;

namespace CFR.CUTSCENE
{
    public class BackToStageSelectButton : SceneButtonFunction
    {
        [SerializeField] bool isIntro = false;
        int logNo;

        private void Start() 
        {
            logNo = PlayerPrefs.GetInt(Globals.lastLog);
            
            if(isIntro) return;
            
            PlayerPrefs.SetInt(Globals.lastStage, 0);
            PlayerPrefs.SetInt(Globals.lastLog, logNo + 1);
        }

        protected override string TargetSceneName()
        {
            if(isIntro) return (logNo).ToString() + "-1";
            else return "Stage Select";
        }
    }
}
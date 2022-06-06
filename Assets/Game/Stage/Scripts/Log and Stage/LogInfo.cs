using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CFR.STAGE
{
    [CreateAssetMenu(fileName = "LogInfo", menuName = "Levels/Log", order = 0)]
    public class LogInfo : ScriptableObject
    {
        [SerializeField] int logNumber = 1;
        [SerializeField] string logName = "";
        [SerializeField] StageInfo[] stages = new StageInfo[0];
        [SerializeField] string introCutsceneName = "";
        [SerializeField] string outroCutsceneName = "";

        public string GetLogName() => logName;
        public int GetLogNumber() => logNumber;
        public bool HasCompleted()
        {
            StageInfo lastStage = stages[stages.Length - 1];
            return lastStage.HasCompleted();
        }
        public IEnumerable<StageInfo> GetStages() { return stages; }
        public int GetStageCount() { return stages.Length; }
        public string GetIntroCutsceneName() => introCutsceneName;
        public string GetOutroCutsceneName() => outroCutsceneName;
        public bool IsStageLast(StageInfo _stage)
        {
            if(!stages.Contains(_stage)) return false;
            return stages.Last() == _stage;
        }
    }
}
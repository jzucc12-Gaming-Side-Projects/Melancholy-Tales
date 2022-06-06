using UnityEngine;

namespace CFR.STAGE
{
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "StageInfo", menuName = "Levels/Stage", order = 1)]
    public class StageInfo : ScriptableObject 
    {
        [SerializeField] string stageName = "";
        public int GetLogNo() => int.Parse(name.Split('-')[0]);
        public int GetStageNo() => int.Parse(name.Split('-')[1]);
        public string GetStageID() => GetLogNo() + "-" + GetStageNo();
        public string GetStageName() => stageName;
        public string GetFullName() => GetStageID() + ": " + GetStageName();
        public string GetCompletionKey() => GetStageID() + " Complete";
        public bool HasCompleted() => PlayerPrefs.GetInt(GetCompletionKey()) == 1;
    }
}

using UnityEngine;
using JZ.CORE;

namespace CFR.STAGE
{
    public class StagePrerequisite : HasPrerequisite
    {
        [SerializeField] StageInfo stagePrereq = null;

        private void Awake()
        {
            if(stagePrereq) Check(stagePrereq.HasCompleted());
        }

        public void AddStage(StageInfo _stage)
        {
            stagePrereq = _stage;
            Check(_stage.HasCompleted());
        }
    }
}
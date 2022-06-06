using JZ.CORE;
using UnityEngine;

namespace CFR.STAGE
{
    public class LogPrerequisite : HasPrerequisite
    {
        [SerializeField] LogInfo logPrereq = null;

        private void Awake()
        {
            if(logPrereq) Check(logPrereq.HasCompleted());
        }

        public void AddLog(LogInfo _log)
        {
            logPrereq = _log;
            Check(_log.HasCompleted());
        }
    }
}

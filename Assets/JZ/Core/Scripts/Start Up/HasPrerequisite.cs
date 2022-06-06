using UnityEngine;

namespace JZ.CORE
{
    public class HasPrerequisite : MonoBehaviour
    {
        protected void Check(bool _show)
        {
            if (DevMode.inDevMode) return;
            gameObject.SetActive(_show);
        }
    }
}
using UnityEngine;

namespace JZ.BUTTON
{
    public class QuitGameButton : ButtonFunction
    {
        protected override void Awake()
        {
            base.Awake();
            #if UNITY_WEBGL
            gameObject.SetActive(false);
            #endif
        }

        public override void OnClick()
        {
            Application.Quit();
        }
    }
}
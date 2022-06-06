using UnityEngine;

namespace JZ.BUTTON
{
    public class SceneChangeButton : SceneButtonFunction
    {
        [SerializeField] string targetScene;
        protected override string TargetSceneName() => targetScene;
        public void SetSceneName(string _scene) { targetScene = _scene; }
    }
}
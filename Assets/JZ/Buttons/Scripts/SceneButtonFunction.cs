using UnityEngine;
using JZ.CORE;
using UnityEngine.SceneManagement;

namespace JZ.BUTTON
{
    public abstract class SceneButtonFunction : ButtonFunction
    {
        [SerializeField] AnimType animationType = AnimType.fade;
        bool animateTransition => animationType != AnimType.none;

        public override void OnClick()
        {
            if(animateTransition)
            {
                if(TargetSceneIndex() != -1) 
                    FindObjectOfType<SceneTransitionManager>().TransitionToScene(TargetSceneIndex(), animationType);
                else
                    FindObjectOfType<SceneTransitionManager>().TransitionToScene(TargetSceneName(), animationType);
            }
            else
            {
                if(TargetSceneIndex() != -1) 
                    SceneManager.LoadSceneAsync(TargetSceneIndex());
                else
                    SceneManager.LoadSceneAsync(TargetSceneName());
            }
        }

        protected virtual int TargetSceneIndex() { return -1; }
        protected virtual string TargetSceneName() { return ""; }
    }
}
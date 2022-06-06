using JZ.CORE;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JZ.BUTTON
{
    public class ReloadSceneButton : SceneButtonFunction
    {
        protected override int TargetSceneIndex() => SceneManager.GetActiveScene().buildIndex;
    }
}
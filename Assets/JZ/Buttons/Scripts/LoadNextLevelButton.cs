using UnityEngine.SceneManagement;

namespace JZ.BUTTON
{
    public class LoadNextLevelButton : SceneButtonFunction
    {
        protected override int TargetSceneIndex() => SceneManager.GetActiveScene().buildIndex + 1;
    }
}
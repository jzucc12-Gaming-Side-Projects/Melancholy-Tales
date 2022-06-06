using UnityEngine;
using JZ.BUTTON;

namespace CFR.BUTTON
{
    public class EndGameButton : SceneButtonFunction
    {
        protected override string TargetSceneName()
        {
            if(PlayerPrefs.GetInt(Globals.beatGameKey, 0) == 0)
            {
                PlayerPrefs.SetInt(Globals.beatGameKey, 1);
                return "Main Menu";
            }
            else
            {
                return "Stage Select";
            }
        }
    }
}

using JZ.CORE;
using UnityEngine;

namespace JZ.AUDIO
{
    public class BackgroundMusicPlayer : AudioManager
    {
        static bool isLocked = false;


        protected override void OnEnable()
        {
            base.OnEnable();
            SceneTransitionManager.StartTransitionIn += PlayAgain;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            SceneTransitionManager.StartTransitionIn -= PlayAgain;
        }

        public void SetLock(bool _lock) 
        { 
            isLocked = _lock; 
        }

        public static bool IsLocked() { return isLocked; }

        private void PlayAgain()
        {
            if(!IsPlaying("Background Music") && !SceneTransitionManager.ActiveSceneIsCutscene())
                Play("Background Music");
        }

        public void ChangeBackgroundTrack(string _name, AudioClip _clip)
        {
            if(isLocked) return;
            ChangeClip(_name, _clip);
        }
    }
}
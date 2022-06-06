using UnityEngine;

namespace JZ.AUDIO
{
    public class BackgroundMusicSetter : MonoBehaviour
    {
        [SerializeField] AudioClip backgroundTrack;
        [SerializeField] bool autoSet = true;
        BackgroundMusicPlayer bgPlayer;

        private void Awake() 
        {
            bgPlayer = FindObjectOfType<BackgroundMusicPlayer>();
        }
        
        private void Start()
        {
            if(autoSet) SetTrack();
        }

        public void SetTrack()
        {
            if (bgPlayer.HasClip("Background Music", backgroundTrack)) return;
            bgPlayer.ChangeBackgroundTrack("Background Music", backgroundTrack);
        }

        public void OverridePlayer(bool _lockIn)
        {
            bgPlayer.SetLock(false);
            SetTrack();
            bgPlayer.SetLock(_lockIn);
        }

        public bool IsPlayingMine()
        {
            if(!bgPlayer)
            {
                bgPlayer = FindObjectOfType<BackgroundMusicPlayer>();
                if(!bgPlayer) return false;
            }

            return bgPlayer.GetSound("Background Music").clip == backgroundTrack;
        }
    }
}

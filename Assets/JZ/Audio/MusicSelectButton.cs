using JZ.AUDIO;
using TMPro;
using UnityEngine;

namespace JZ.BUTTON
{
    public class MusicSelectButton : ButtonFunction
    {
        [SerializeField] TextMeshProUGUI currentlyPlayingHeader = null;
        [SerializeField] TextMeshProUGUI songNameHeader = null;
        [SerializeField] TextMeshProUGUI artistHeader = null;

        [SerializeField] TextMeshProUGUI buttonName = null;
        [SerializeField] string trackTitle = "";
        [SerializeField] string songName = "";
        [SerializeField] string artist = "";
        BackgroundMusicSetter bgSetter;

        protected override void Awake()
        {
            base.Awake();
            bgSetter = GetComponent<BackgroundMusicSetter>();
        }
        
        private void Start()
        {
            buttonName.text = trackTitle;
        }

        public override void OnClick()
        {
            bgSetter.OverridePlayer(true);
            DisplayTrackInfo();
        }

        public void DisplayTrackInfo()
        {
            currentlyPlayingHeader.text = "Currently Playing: " + trackTitle;
            songNameHeader.text = "Song Title: " + songName;
            artistHeader.text = "Artist: " + artist;
        }

        public void ResetToMe()
        {
            bgSetter.OverridePlayer(false);
            currentlyPlayingHeader.text = "";
            songNameHeader.text = "Default Music Playing";
            artistHeader.text = "";
        }

        public bool IsPlayingMine()
        {
            return bgSetter.IsPlayingMine();
        }
    }
}
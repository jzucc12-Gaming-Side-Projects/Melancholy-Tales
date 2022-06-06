using JZ.AUDIO;
using JZ.INPUT;
using UnityEngine;
using UnityEngine.UI;
using JZ.BUTTON;
using JZ.MENU;
using CFR.STAGE;

namespace CFR.UI
{
    public class MusicSelectUI : MonoBehaviour
    {
        #region //Components
        [SerializeField] MenuManager firstNoticeBox = null;
        [SerializeField] MusicSelectButton[] trackButtons = new MusicSelectButton[0];
        [SerializeField] LogInfo[] nextPrereq = new LogInfo[0];
        [SerializeField] Button nextButton = null;
        [SerializeField] Button previousButton = null;
        AudioManager sfxPlayer = null;
        MenuingInputSystem menuSystem;
        #endregion

        int activeTrack = 0;
        MusicSelectButton activeButton => trackButtons[activeTrack];
        LogInfo activePrereq => nextPrereq[activeTrack];


        #region //Monobehaviour
        private void Awake() 
        {
            menuSystem = new MenuingInputSystem(new GeneralInputs());
            sfxPlayer = GetComponent<AudioManager>();
        }

        private void Start() 
        {
            foreach(var track in trackButtons)
                track.gameObject.SetActive(false);

            for(int ii = 0; ii < trackButtons.Length; ii++)
            {
                if(!trackButtons[ii].IsPlayingMine()) continue;
                ShowTrack(ii);
                if(BackgroundMusicPlayer.IsLocked()) activeButton.DisplayTrackInfo();
                break;
            }
        }

        private void OnEnable() 
        {
            nextButton.onClick.AddListener(Next);
            previousButton.onClick.AddListener(Previous);
            menuSystem.Activate(true);
        }

        private void OnDisable() 
        {
            nextButton.onClick.RemoveListener(Next);
            previousButton.onClick.RemoveListener(Previous);
            menuSystem.Activate(false);
        }

        private void Update() 
        {
            if(menuSystem.xNav > 0)
            {
                menuSystem.ExpendXDir();
                Next();
            }
            else if(menuSystem.xNav < 0)
            {
                menuSystem.ExpendXDir();
                Previous();
            }

            if(menuSystem.selected)
            {
                menuSystem.ExpendMenuSelect();
                activeButton.OnClick();
            }
        }
        #endregion

        #region //Button methods
        void Next()
        {
            if(!nextButton.gameObject.activeInHierarchy) return;

            sfxPlayer.Play("Menu Move");
            ShowTrack(activeTrack + 1);
        }

        void Previous()
        {
            if(!previousButton.gameObject.activeInHierarchy) return;

            sfxPlayer.Play("Menu Move");
            ShowTrack(activeTrack - 1);
        }

        public void Close()
        {
            sfxPlayer.Play("Menu Select");
            enabled = false;
        }

        public void Open()
        {
            enabled = true;
        }
        #endregion
    
        void ShowTrack(int _number)
        {
            activeButton.gameObject.SetActive(false);
            activeTrack = _number;
            activeButton.gameObject.SetActive(true);

            previousButton.gameObject.SetActive(activeTrack > 0);
            if(activeTrack < trackButtons.Length - 1)
            {
                bool activate = activePrereq.HasCompleted() || DevMode.inDevMode;
                nextButton.gameObject.SetActive(activate);
            }
            else
                nextButton.gameObject.SetActive(false);

        }
    }
}
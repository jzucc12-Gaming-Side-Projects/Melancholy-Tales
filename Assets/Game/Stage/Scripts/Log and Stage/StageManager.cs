using System.Collections;
using CFR.CUTSCENE;
using CFR.INPUT;
using CFR.ITINERARY;
using CFR.LZ;
using CFR.SHIP;
using JZ.AUDIO;
using JZ.BUTTON;
using JZ.MENU;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CFR.STAGE
{
    public class StageManager : MonoBehaviour
    {
        #region //Cached menus
        [SerializeField] MenuManager victoryScreen = null;
        [SerializeField] MenuManager defeatScreen = null;
        [SerializeField] MenuManager pauseScreen = null;
        #endregion

        #region //Other cached components
        [SerializeField] TextMeshProUGUI headerText = null;
        [SerializeField] float endGameFadeVolume = 0.3f;
        StageInfo currentStage;
        AudioManager audioManager;
        ShipController shipController;
        ItineraryInputManager itineraryInputs;
        LandingZone[] LZs;
        #endregion

        float waitTime = 2f;
        bool isLastStageInLog = false;
        public bool ended { get; private set; }
        int logNum;


        #region//Monobehaviour
        private void Awake()
        {
            ended = false;
            string sceneName = SceneManager.GetActiveScene().name;
            string logNumString = sceneName.Split('-')[0];
            logNum = int.Parse(logNumString);
            LogInfo log = Resources.Load<LogInfo>("Logs/Log " + logNumString);
            currentStage = Resources.Load<StageInfo>("Stages/Log " + logNumString + "/" + sceneName);
            if(currentStage == null)
                currentStage = Resources.Load<StageInfo>("Stages/Log 1/1-1");
            else
                isLastStageInLog = log.IsStageLast(currentStage);

            audioManager = GetComponent<AudioManager>();
            shipController = FindObjectOfType<ShipController>();
            LZs = FindObjectsOfType<LandingZone>();
            itineraryInputs = FindObjectOfType<ItineraryInputManager>();
        }

        private void Start() 
        {
            PlayerPrefs.SetInt(Globals.lastLog, currentStage.GetLogNo());
            PlayerPrefs.SetInt(Globals.lastStage, currentStage.GetStageNo());
            if(currentStage) headerText.text = currentStage.GetFullName();
            Time.timeScale = 1;
        }

        private void OnEnable()
        {
            LandingZone.Victory += Victory;
            StageInputManager.OnPause += OnPause;
            ShipMoveSM.stateChanged += EndGameCheck;
            defeatScreen.GetComponent<DefeatScreenManager>().Defeated += Defeat;
            Itinerary.Rewind += Rewound;
        }

        private void OnDisable()
        {
            LandingZone.Victory -= Victory;
            StageInputManager.OnPause -= OnPause;
            ShipMoveSM.stateChanged -= EndGameCheck;
            defeatScreen.GetComponent<DefeatScreenManager>().Defeated -= Defeat;
            Itinerary.Rewind -= Rewound;
        }

        private void Update() 
        {
            if(DevMode.inDevMode && Input.GetKeyDown(KeyCode.Z))
                Victory();    
        }
        #endregion

        #region//End game
        void EndGameCheck(ShipMoveState _newState)
        {
            if (_newState is LandingState)
            {
                foreach (LandingZone lz in LZs)
                    lz.Landing(shipController.currentLZ);
            }
            else if(_newState is MovingState)
            {
                foreach (LandingZone lz in LZs)
                    lz.TakingOff(shipController.currentLZ);
            }
        }

        IEnumerator EndGameWait(bool _victory)
        {
            FindObjectOfType<BackgroundMusicPlayer>().FadeTo("Background Music", waitTime * 0.5f, endGameFadeVolume);
            yield return new WaitForSeconds(waitTime);
            FindObjectOfType<BackgroundMusicPlayer>().FadeTo("Background Music", waitTime * 0.5f, 1);
            if(_victory)
            {
                victoryScreen.StartUp();

                if(!isLastStageInLog) yield break;
                var btnOne = victoryScreen.GetComponentsInChildren<MenuMember>()[0];
                btnOne.GetComponent<LoadNextLevelButton>().enabled = false;
                
                if(logNum == 10)
                {
                    btnOne.GetComponent<SceneChangeButton>().enabled = true;
                    btnOne.GetComponentInChildren<TextMeshProUGUI>().text = "SAVE BF";
                }
                else
                {
                    btnOne.GetComponent<BackToStageSelectButton>().enabled = true;
                    btnOne.GetComponentInChildren<TextMeshProUGUI>().text = "Finish Log";
                }
            }
            else
                defeatScreen.StartUp();
        }

        void Victory()
        {
            StartCoroutine(EndGameWait(true));
            PlayerPrefs.SetInt(currentStage.GetCompletionKey(),1);
            audioManager.Play("Level Complete");
            shipController.GetComponent<Animation>().Play();
            EndGame();
        }

        void Defeat()
        {
            StartCoroutine(EndGameWait(false));
            EndGame();
        }

        void EndGame()
        {
            ended = true;
            FindObjectOfType<ActiveLZController>().ForceOff();
            FindObjectOfType<PlayerInputManager>().LockInputs(true);
            FindObjectOfType<StageInputManager>().EndGameState(true);
            itineraryInputs.DeactivateToggle();
        }

        void Rewound(bool _fromDefeat)
        {
            if(!_fromDefeat) return;
            ended = false;
            FindObjectOfType<ActiveLZController>().Lock(false);
            FindObjectOfType<PlayerInputManager>().LockInputs(false);
            FindObjectOfType<StageInputManager>().EndGameState(false);
            if(shipController.overLandingZone)
                FindObjectOfType<ActiveLZController>().StartUp(shipController.currentLZ);
        }
        #endregion

        #region//Pausing
        void OnPause(bool _pause)
        {
            Time.timeScale = (_pause ? 0 : 1);
            if (_pause)
            {
                foreach(AudioManager am in FindObjectsOfType<AudioManager>())
                    am.StopAllSFX();

                pauseScreen.StartUp();
            }
            else
                pauseScreen.ShutDown();
        }
        #endregion
    }
}
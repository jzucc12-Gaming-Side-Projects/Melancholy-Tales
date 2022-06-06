using JZ.AUDIO;
using JZ.INPUT;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CFR.STAGE
{
    public class RulesGlossaryUI : MonoBehaviour
    {
        #region //Components
        [SerializeField] GameObject[] rules = new GameObject[0];
        [SerializeField] StageInfo[] nextPrereq = new StageInfo[0];
        [SerializeField] Button closeButton = null;
        [SerializeField] Button nextButton = null;
        [SerializeField] Button previousButton = null;
        AudioManager sfxPlayer = null;
        MenuingInputSystem menuSystem;
        #endregion

        int activeWindow = 0;
        GameObject activeRule => rules[activeWindow];
        StageInfo activePrereq => nextPrereq[activeWindow];
        int logNum = 0;
        int stageNum = 0;


        #region //Monobehaviour
        private void Awake() 
        {
            menuSystem = new MenuingInputSystem(new GeneralInputs());
            sfxPlayer = GetComponent<AudioManager>();
            string sceneName = SceneManager.GetActiveScene().name;
            logNum = int.Parse(sceneName.Split('-')[0]);
            stageNum = int.Parse(sceneName.Split('-')[1]);
        }

        private void Start() 
        {
            for(int ii = 0; ii < rules.Length; ii++)
            {
                rules[ii].SetActive(true);
                Canvas.ForceUpdateCanvases();
                foreach(var layout in activeRule.GetComponentsInChildren<VerticalLayoutGroup>())
                {
                    layout.enabled = false;
                    layout.enabled = true;
                }

                foreach(var layout in activeRule.GetComponentsInChildren<HorizontalLayoutGroup>())
                {
                    layout.enabled = false;
                    layout.enabled = true;
                }
                rules[ii].SetActive(false);
            }    
            SetRule(0);
            enabled = false;
        }

        private void OnEnable() 
        {
            GetComponent<Canvas>().enabled = true;
            closeButton.onClick.AddListener(Close);
            nextButton.onClick.AddListener(Next);
            previousButton.onClick.AddListener(Previous);
            menuSystem.Activate(true);
            foreach(var rule in rules)
                rule.SetActive(false);

            SetRule(0);
        }

        private void OnDisable() 
        {
            GetComponent<Canvas>().enabled = false;
            closeButton.onClick.RemoveListener(Close);
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
                closeButton.onClick?.Invoke();
            }
        }
        #endregion

        #region //Button methods
        void Next()
        {
            if(!nextButton.gameObject.activeInHierarchy) return;

            sfxPlayer.Play("Menu Move");
            SetRule(activeWindow + 1);
        }

        void Previous()
        {
            if(!previousButton.gameObject.activeInHierarchy) return;

            sfxPlayer.Play("Menu Move");
            SetRule(activeWindow - 1);
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
    
        void SetRule(int _number)
        {
            activeRule.SetActive(false);
            activeWindow = _number;
            activeRule.SetActive(true);

            previousButton.gameObject.SetActive(activeWindow > 0);
            if(activeWindow < rules.Length - 1)
            {
                int logDiff = logNum - activePrereq.GetLogNo();
                int stageDiff = stageNum - activePrereq.GetStageNo();
                bool activate = logDiff > 0 || (logDiff == 0 && stageDiff >= 0);
                nextButton.gameObject.SetActive(activate);
            }
            else
                nextButton.gameObject.SetActive(false);

        }
    }
}
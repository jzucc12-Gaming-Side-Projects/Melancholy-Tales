using System;
using UnityEngine;

namespace JZ.CORE
{
    public class GameStartUp : MonoBehaviour
    {
        public static event Action SetUpDone;

        private void Awake()
        {
            Application.targetFrameRate = 60;
            Application.runInBackground = true;
            InitiateVolume();
            GameSpecificStartUp();
            SetUpDone?.Invoke();
        }

        private void Update() 
        {
            if(!DevMode.inDevMode) return;

            if(Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.W) & Input.GetKey(KeyCode.E) && Input.GetKeyDown(KeyCode.R))
            {
                ResetData();
            }
        }

        void InitiateVolume()
        {
            if(!PlayerPrefs.HasKey(GameSettings.masterVolKey))
            {
                PlayerPrefs.SetFloat(GameSettings.masterVolKey, 0.5f);
                PlayerPrefs.SetFloat(GameSettings.musicVolKey, 0.5f);
                PlayerPrefs.SetFloat(GameSettings.sfxVolKey, 0.5f);
            }
            else
            {
                GameSettings.SetVolume(VolumeType.master, PlayerPrefs.GetFloat(GameSettings.masterVolKey));
                GameSettings.SetVolume(VolumeType.music, PlayerPrefs.GetFloat(GameSettings.musicVolKey));
                GameSettings.SetVolume(VolumeType.sfx, PlayerPrefs.GetFloat(GameSettings.sfxVolKey));
            }
        }
    
        void GameSpecificStartUp()
        {
            if(!PlayerPrefs.HasKey(Globals.lastLog))
            {
                PlayerPrefs.SetInt(Globals.lastLog, 1);
                PlayerPrefs.SetInt(Globals.lastStage, 0);
            }

            if(!PlayerPrefs.HasKey(Globals.assistKey))
                PlayerPrefs.SetInt(Globals.assistKey, 1);
        }
    
        void ResetData()
        {
            Debug.Log("reset data");
            PlayerPrefs.DeleteAll();
            GameSpecificStartUp();
        }
    }
}

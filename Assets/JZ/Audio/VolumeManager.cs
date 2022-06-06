using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JZ.AUDIO
{
    public class VolumeManager : MonoBehaviour
    {
        Slider mySlider;
        [SerializeField] VolumeType myType = VolumeType.master;
        float vol;
        public static event Action VolumeUpdated;

        #region //Monobehaviour
        private void Awake() 
        {
            mySlider = GetComponentInChildren<Slider>();
        }

        private void Start() 
        {
            mySlider.value = GameSettings.GetVolume(myType);
            vol = mySlider.value;
        }

        private void OnEnable() 
        {
            mySlider.onValueChanged.AddListener(SetVolume);
        }

        private void OnDisable() 
        {
            mySlider.onValueChanged.RemoveListener(SetVolume);
        }
        #endregion

        #region //Modify Volume
        void SetVolume(float _newValue)
        {
            GameSettings.SetVolume(myType, _newValue);
            vol = _newValue;
            SaveVolume();
            VolumeUpdated?.Invoke();
        }

        public void ResetVolume()
        {
            float defaultVol = GameSettings.GetDeffaultVolume(myType);
            mySlider.value = defaultVol;
            SetVolume(defaultVol);
        }

        public void SaveVolume()
        {
            switch(myType)
            {
                case VolumeType.master:
                    PlayerPrefs.SetFloat(GameSettings.masterVolKey, vol);
                    return;
                case VolumeType.music:
                    PlayerPrefs.SetFloat(GameSettings.musicVolKey, vol);
                    return;
                case VolumeType.sfx:
                    PlayerPrefs.SetFloat(GameSettings.sfxVolKey, vol);
                    return;
            }
        }
        #endregion
    }
}

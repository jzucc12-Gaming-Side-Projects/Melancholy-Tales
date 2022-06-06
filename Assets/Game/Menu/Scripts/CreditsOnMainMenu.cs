using JZ.AUDIO;
using UnityEngine;
using UnityEngine.UI;

namespace CFR.MENU
{
    public class CreditsOnMainMenu : MonoBehaviour
    {
        [SerializeField] Button creditsButton = null;
        [SerializeField] AudioManager mainMenuSFX = null;

        void Start()
        {
            if(PlayerPrefs.GetInt(Globals.beatGameKey, 0) == 0) return;
            if(PlayerPrefs.GetInt(Globals.hasSeenCreditsKey, 0) == 1) return;

            PlayerPrefs.SetInt(Globals.hasSeenCreditsKey, 1);
            creditsButton.onClick?.Invoke();
            mainMenuSFX.Stop("menu select");
        }
    }

}
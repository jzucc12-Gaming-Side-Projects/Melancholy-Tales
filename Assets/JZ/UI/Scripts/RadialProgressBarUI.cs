using UnityEngine;
using UnityEngine.UI;

namespace JZ.UI
{
    public class RadialProgressBarUI : ProgressBarUI
    {
        [SerializeField] Image radialImage = null;
        
        protected override void UpdateProgressBar(float _percentage)
        {
            radialImage.fillAmount = _percentage;
        }
    }
}
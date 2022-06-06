using UnityEngine;

namespace JZ.UI
{
    public class LinearProgressBarUI : ProgressBarUI
    {
        [SerializeField] Transform progressBar;

        protected override void UpdateProgressBar(float _percentage)
        {
            progressBar.localScale = new Vector2(_percentage,1);
        }
    }
}
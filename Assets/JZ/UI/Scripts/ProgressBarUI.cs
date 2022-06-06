using UnityEngine;
using JZ.CORE;
using System.Collections.Generic;

namespace JZ.UI
{
    public abstract class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] List<GameObject> progressorObjects = new List<GameObject>();
        List<IProgressible> progressors = new List<IProgressible>();
        IProgressible progress;

        private void Awake() 
        {
            foreach(GameObject obj in progressorObjects)
                progressors.Add(obj.GetComponent<IProgressible>());
        }

        private void Update() 
        {
            float progress = GetMaxPercentage();
            UpdateProgressBar(progress);
        }

        float GetMaxPercentage()
        {
            float maxProgress = 0;
            foreach(IProgressible progress in progressors)
                maxProgress = Mathf.Max(maxProgress, progress.GetProgressPercentage());

            return maxProgress;
        }

        protected abstract void UpdateProgressBar(float _percentage);
    }
}

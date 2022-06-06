using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace  JZ.CORE
{
    //Originally from Brackeys//
    public class SceneTransitionManager : MonoBehaviour
    {
        [SerializeField] Animator[] animators = null;
        Animator currentAnimator = null;
        public static event Action<bool> StartTransitionOut;
        public static event Action StartTransitionIn;
        static SceneType currentSceneType;
        bool transitionRoutineRunning = false;

        private void Awake() 
        {
            currentSceneType = GetSceneType(SceneManager.GetActiveScene().path);
        }

        #region //Transitions
        public void TransitionToScene(string _name, AnimType _type)
        {
            if(currentAnimator == null)
                currentAnimator = animators[(int)_type];
            int index = GetSceneIndexFromName(_name);
            StopAllCoroutines();
            StartCoroutine(TransitionCoroutine(index));
        }

        public void TransitionToScene(int _sceneIndex, AnimType _type)
        {
            if(currentAnimator == null)
                currentAnimator = animators[(int)_type];
            StopAllCoroutines();
            StartCoroutine(TransitionCoroutine(_sceneIndex));
        }

        private static SceneType GetSceneType(string _path)
        {
            if(_path.Contains("Menu")) return SceneType.menu;
            else if(_path.Contains("Cutscene")) return SceneType.cutscene;
            else return SceneType.level;
        }

        public static bool ActiveSceneIsLevel()
        {
            return GetSceneType(SceneManager.GetActiveScene().path) == SceneType.level;
        }

        public static bool ActiveSceneIsCutscene()
        {
            return GetSceneType(SceneManager.GetActiveScene().path) == SceneType.cutscene;
        }

        public static bool ActiveSceneIsMenu()
        {
            return GetSceneType(SceneManager.GetActiveScene().path) == SceneType.menu;
        }

        IEnumerator TransitionCoroutine(int _index)
        {
            //Fade out
            Time.timeScale = 1;
            SceneType targetType = GetSceneType(SceneUtility.GetScenePathByBuildIndex(_index));
            if(!transitionRoutineRunning) currentAnimator.SetTrigger("TransitionOut");
            transitionRoutineRunning = true;
            StartTransitionOut?.Invoke(currentSceneType != targetType);

            //Transition
            while(currentAnimator.GetCurrentAnimatorClipInfoCount(0) == 0)
                yield return null;

            float waitDuration = currentAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.averageDuration;
            float buffer = 0.5f;
            yield return new WaitForSeconds(waitDuration + buffer);
            yield return SceneManager.LoadSceneAsync(_index);

            //Fade in
            currentSceneType = targetType;
            StartTransitionIn?.Invoke();
            yield return null;
            currentAnimator.SetTrigger("TransitionIn");
            transitionRoutineRunning = false;
            currentAnimator = null;
        }
        #endregion

        #region //Get scene info
        string GetSceneNameFromIndex(int _index)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(_index);
            string withExtension = path.Substring(path.LastIndexOf('/') + 1);
            string withoutExtension = withExtension.Substring(0, withExtension.LastIndexOf('.'));
            return withoutExtension;
        }

        int GetSceneIndexFromName(string _name)
        {
            for(int ii = 0; ii < SceneManager.sceneCountInBuildSettings; ii++)
            {
                string sceneName = GetSceneNameFromIndex(ii);
                if(sceneName != _name) continue;
                return ii;
            }

            return -1;
        }
        #endregion

        public bool IsTransitioning()
        {
            if(currentAnimator == null) return false;
            if(transitionRoutineRunning) return true;
            return currentAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
        }
    }

    public enum SceneType
    {
        menu = 0,
        level = 1,
        cutscene = 2
    }

    public enum AnimType
    {
        none = -1,
        fade = 0,
        longFade = 1,
        ship = 2
    }
}

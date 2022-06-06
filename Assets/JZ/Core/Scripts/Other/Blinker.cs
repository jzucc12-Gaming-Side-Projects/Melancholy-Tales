using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace JZ.CORE
{
    public class Blinker : MonoBehaviour
    {
        [SerializeField] float blinkRate = 1f;
        [SerializeField] float fadeTimer = 0.1f;
        [SerializeField] Graphic target = null;
        IEnumerator fadeRoutine;
        float currTimer;

        void FixedUpdate()
        {
            if(currTimer >= blinkRate)
            {
                currTimer = 0;
                fadeRoutine = Fade();
                StartCoroutine(fadeRoutine);
            }

            if(fadeRoutine != null) return;
            currTimer += Time.deltaTime;
        }

        IEnumerator Fade()
        {
            Color currColor = target.color;
            float startAlpha = currColor.a;
            float targetAlpha = startAlpha == 0 ? 1 : 0;
            float currAlpha = currColor.a;

            float currFadeTimer = 0;
            while(currFadeTimer < fadeTimer)
            {
                float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, Mathf.Min(currFadeTimer/fadeTimer,1));
                currColor.a = newAlpha;
                target.color = currColor;
                currFadeTimer += Time.deltaTime;
                yield return null;
            }

            currColor.a = targetAlpha;
            target.color = currColor;
            fadeRoutine = null;
        }
    }
}
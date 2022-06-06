using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace JZ.CORE
{
    public class IntroCutsceneBounceBack : MonoBehaviour
    {
        [SerializeField] float waitTimerInSeconds = 60f;

        private void Awake() 
        {
            StartCoroutine(WaitToBouncBack());
        }

        IEnumerator WaitToBouncBack()
        {
            float currTimer = 0;
            while(currTimer < waitTimerInSeconds)
            {
                yield return null;
                currTimer += Time.deltaTime;
                bool btnPress = Input.anyKeyDown;

                if(Gamepad.current != null)
                {
                    foreach(InputControl control in Gamepad.current.allControls)
                    {
                        if(!(control is ButtonControl)) continue;
                        if(!control.IsPressed() || control.synthetic) continue;
                        btnPress = true;
                        break;
                    }
                }


                if(btnPress) currTimer = 0;
            }

            FindObjectOfType<SceneTransitionManager>().TransitionToScene("Intro Cutscene", AnimType.longFade);
        }
    }
}

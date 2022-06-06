using JZ.AUDIO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace JZ.CORE
{
    public class StartMenu : MonoBehaviour
    {

        static bool started = false;

        private void Start() 
        {
            if(!started) 
            {
                FindObjectOfType<Button>().onClick.AddListener(StartSFX);
                return;
            }
            FindObjectOfType<Button>().onClick?.Invoke();
        }

        void StartSFX()
        {
            GetComponent<AudioManager>().Play("Start");
        }

        void Update()
        {
            if(started) return;
            
            if(Input.anyKeyDown)
            {
                FindObjectOfType<Button>().onClick?.Invoke();
                started = true;
                return;
            }

            if(Gamepad.current == null) return;
            foreach(InputControl control in Gamepad.current.allControls)
            {
                if(!control.IsPressed() || control.synthetic) continue;
                FindObjectOfType<Button>().onClick?.Invoke();
                started = true;
                return;
            }
        }
    }
}

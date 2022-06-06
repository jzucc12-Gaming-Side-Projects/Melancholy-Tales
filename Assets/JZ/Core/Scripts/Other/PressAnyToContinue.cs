using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PressAnyToContinue : MonoBehaviour
{
    private void Awake() 
    {
        FindObjectOfType<Button>().onClick?.Invoke();
    }

    void Update()
    {
        if(Input.anyKeyDown)
        {
            FindObjectOfType<Button>().onClick?.Invoke();
            return;
        }

        if(Gamepad.current == null) return;
        foreach(InputControl control in Gamepad.current.allControls)
        {
            if(!control.IsPressed() || control.synthetic) continue;
            FindObjectOfType<Button>().onClick?.Invoke();
            return;
        }
    }
}

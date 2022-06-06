using UnityEngine;
using UnityEngine.InputSystem;

public class FullScreenSwitcher : MonoBehaviour
{
    GeneralInputs inputs;
    private void Awake() 
    {
        inputs = new GeneralInputs();
    }

    private void OnEnable() 
    {
        inputs.Options.ToggleFullscreen.Enable();  
        inputs.Options.ToggleFullscreen.started += ToggleFS;  
    }

    private void OnDisable() 
    {
        inputs.Options.ToggleFullscreen.Disable();    
        inputs.Options.ToggleFullscreen.started -= ToggleFS;  
    }

    void ToggleFS(InputAction.CallbackContext _context)
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}

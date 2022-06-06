using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class DeviceChecker : MonoBehaviour
{
    PlayerInput playerInput;


    private void Awake() 
    {
        playerInput = GetComponent<PlayerInput>();    
    }

    private void OnEnable()
    {
        InputUser.onChange += DeviceCheck;
    }

    private void OnDisable()
    {
        InputUser.onChange -= DeviceCheck;
    }

    void DeviceCheck(InputUser user, InputUserChange change, InputDevice dvc)
    {
        if (change == InputUserChange.ControlSchemeChanged)
        {
            GameSettings.isUsingGamepad = playerInput.currentControlScheme == "Gamepad";
        }
    }
}

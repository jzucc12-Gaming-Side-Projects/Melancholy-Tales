using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
#if !UNITY_WEBGL
using UnityEngine.InputSystem.Switch;
#endif
using UnityEngine.UI;

public static class GameSettings
{
    #region //Gameplay
    public static bool isUsingGamepad = false;
    static GamepadType lastType = GamepadType.none;

    public static GamepadType CurrentGamepad()
    {
        if(!isUsingGamepad)
        {
            if(lastType != GamepadType.none && Gamepad.current == null) 
                return lastType;
        }

        if (Gamepad.current is DualShockGamepad)
            lastType = GamepadType.sony;
        #if !UNITY_WEBGL
        else if (Gamepad.current is SwitchProControllerHID)
            lastType = GamepadType.nSwitch;
        #endif
        else
            lastType = GamepadType.xbox;

        return lastType;
    }
    #endregion

    #region //Visuals
    public static Color color1 = Color.cyan;
    public static Color color2 = Color.magenta;
    public static Color defaultColor = Color.clear;

    public static void UpdateContentSizeFitter(GameObject _target)
    {
        Canvas.ForceUpdateCanvases();
        foreach(var layout in _target.GetComponentsInChildren<VerticalLayoutGroup>())
        {
            layout.enabled = false;
            layout.enabled = true;
        }

        foreach(var layout in _target.GetComponentsInChildren<HorizontalLayoutGroup>())
        {
            layout.enabled = false;
            layout.enabled = true;
        }
    }
    #endregion

    #region //Audio
    public const string masterVolKey = "Master Volume";
    public const string musicVolKey = "Music Volume";
    public const string sfxVolKey = "SFX Volume";

    //Master, Music, SFX
    static float[] volumes = { 0.5f, 0.5f, 0.5f };
    static float[] defaultVolumes = { 0.5f, 0.5f, 0.5f };
    public static float GetVolume(VolumeType _type) { return volumes[(int)_type]; }
    public static float GetDeffaultVolume(VolumeType _type) { return defaultVolumes[(int)_type]; }
    public static void SetVolume(VolumeType _type, float _newVol) { volumes[(int)_type] = _newVol; }
    public static float GetAdjustedVolume(VolumeType _type) 
    { 
        float master = volumes[(int)VolumeType.master];
        float specific = volumes[(int)_type];
        return 2 * master * specific;
    }
    #endregion
}

public enum GamepadType
{
    none = 0,
    xbox = 1,
    sony = 2,
    nSwitch = 3
}

public enum VolumeType
{
    master = 0,
    music = 1,
    sfx = 2
}

public enum TypeHelpMode
{
    none = -1,
    type = 0,
    quirks = 1
}
using UnityEngine;
using UnityEngine.UI;
using JZ.INPUT;

public class TypeHelpSettingsUI : MonoBehaviour
{
    [SerializeField] Button previousArrow = null;
    [SerializeField] Button nextArrow = null;
    [SerializeField] GameObject[] modes = new GameObject[0];
    MenuingInputSystem menuSystem;


    private void Awake() 
    {
        menuSystem = new MenuingInputSystem(new GeneralInputs());
    }

    private void OnEnable() 
    {
        menuSystem.Activate(true);
    }

    private void OnDisable() 
    {
        menuSystem.Activate(false);
    }

    private void Start()
    {
        int startMode = PlayerPrefs.GetInt(Globals.assistKey);
        SetMode(startMode);
    }

    private void Update()
    {
        if(menuSystem.xNav > 0)
        {
            menuSystem.ExpendXDir();
            if(nextArrow.gameObject.activeInHierarchy) NextMode();
        }
        else if(menuSystem.xNav < 0)
        {
            menuSystem.ExpendXDir();
            if(previousArrow.gameObject.activeInHierarchy) PreviousMode();
        }
    }

    public void NextMode()
    {
        int newMode = PlayerPrefs.GetInt(Globals.assistKey, 0) + 1;
        SetMode(newMode);
    }

    public void PreviousMode()
    {
        int newMode = PlayerPrefs.GetInt(Globals.assistKey, 0) - 1;
        SetMode(newMode);
    }

    private void SetMode(int _mode)
    {
        PlayerPrefs.SetInt(Globals.assistKey, _mode);
        for(int ii = 0; ii < modes.Length; ii++)
            modes[ii].SetActive(ii == _mode + 1);

        previousArrow.gameObject.SetActive(_mode + 1 > 0);
        nextArrow.gameObject.SetActive(_mode + 1 < modes.Length - 1);
    }

    public void ResetSetting()
    {
        PlayerPrefs.SetInt(Globals.assistKey, 1);
        SetMode(PlayerPrefs.GetInt(Globals.assistKey));
    }
}

using UnityEngine;

namespace CFR.UI
{
    public class TypeHelpWindowManager : MonoBehaviour
    {
        [SerializeField] BoxType myType = BoxType.baseBox;
        [SerializeField] TypeHelpMode maxAllowableMode = TypeHelpMode.quirks;
        [SerializeField] Transform baseBoxContainer = null;
        [SerializeField] Transform renegadeBoxContainer = null;
        [SerializeField] Transform sneakyBoxContainer = null;
        Transform containerToUse;
        int currentMode;


        private void Start() 
        {
            foreach(Transform box in baseBoxContainer)
                box.gameObject.SetActive(false);

            foreach(Transform box in renegadeBoxContainer)
                box.gameObject.SetActive(false);

            foreach(Transform box in sneakyBoxContainer)
                box.gameObject.SetActive(false);

            switch(myType)
            {
                case BoxType.baseBox:
                default:
                    containerToUse = baseBoxContainer;
                    break;
                case BoxType.renegadeBox:
                    containerToUse = renegadeBoxContainer;
                    break;
                case BoxType.sneakyBox:
                    containerToUse = sneakyBoxContainer;
                    break;
            }

            ChangeShowingBox(GetMode());
        }

        private void Update()
        {
            int newMode = GetMode();
            if(currentMode != newMode)
            {
                ChangeShowingBox(newMode);
            }

            if(DevMode.inDevMode)
                SwapMode();
        }

        private int GetMode()
        {
            return Mathf.Min(PlayerPrefs.GetInt(Globals.assistKey, 0), (int)maxAllowableMode);
        }

        private void ChangeShowingBox(int _newBox)
        {
            for(int ii = 0; ii < containerToUse.childCount; ii++)
            {
                var child = containerToUse.GetChild(ii);
                child.gameObject.SetActive(ii == _newBox);
            }

            currentMode = _newBox;
        }


        //For debugging
        private void SwapMode()
        {
            int newMode = -2;
            if(Input.GetKeyDown(KeyCode.Alpha1))
                newMode = -1;
            else if(Input.GetKeyDown(KeyCode.Alpha2))
                newMode = 0;
            else if(Input.GetKeyDown(KeyCode.Alpha3))
                newMode = 1;
            else if(Input.GetKeyDown(KeyCode.Alpha4))
                newMode = 2;
        
            if(newMode > -2)
                PlayerPrefs.SetInt(Globals.assistKey, newMode);        
        }
    }

    enum BoxType
    {
        baseBox = 0,
        renegadeBox = 1,
        sneakyBox = 2
    }
}
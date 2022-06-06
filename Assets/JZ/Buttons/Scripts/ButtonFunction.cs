using UnityEngine;
using UnityEngine.UI;

namespace JZ.BUTTON
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonFunction : MonoBehaviour
    {
        public Button myButton { get; private set; }

        #region//Monobehaviour
        protected virtual void Awake()
        {
            myButton = GetComponent<Button>();
        }

        protected virtual void OnEnable()
        {
            myButton.onClick.AddListener(OnClick);
        }

        protected virtual void OnDisable()
        {
            myButton.onClick.RemoveListener(OnClick);
        }
        #endregion

        #region//Pointer events
        public abstract void OnClick();
        #endregion
    }
}
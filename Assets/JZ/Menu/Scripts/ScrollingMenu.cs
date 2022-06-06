using UnityEngine;
using UnityEngine.UI;

namespace JZ.MENU
{
    public class ScrollingMenu : MenuManager
    {
        #region//Cached variables
        [Header("Scolling specific")]
        [SerializeField] protected MenuScroller scroller = null;
        [SerializeField] protected ScrollingMenu previousMenu = null;
        [SerializeField] protected ScrollingMenu nextMenu = null;
        [SerializeField] protected Button previousButton = null;
        [SerializeField] protected Button nextButton = null;
        protected bool cantProgressForward = false;
        protected bool cantProgressBackward = false;
        #endregion


        #region //Monobehaviour
        protected override void Start() 
        {
            base.Start();
            if(!nextMenu) nextButton.gameObject.SetActive(false);
            if(!previousMenu) previousButton.gameObject.SetActive(false);
        }

        protected virtual void OnEnable() 
        {
            if(previousMenu) previousButton.onClick.AddListener(PreviousMenu);
            if(nextMenu) nextButton.onClick.AddListener(NextMenu);
        }

        protected virtual void OnDisable() 
        {
            if(previousMenu) previousButton.onClick.RemoveListener(PreviousMenu);
            if(nextMenu) nextButton.onClick.RemoveListener(NextMenu);
        }
        #endregion

        #region //Overrides
        protected override void PassMenuBounds(bool _passLastItem)
        {
            base.PassMenuBounds(_passLastItem);
            if(_passLastItem)
                NextMenu();
            else
                PreviousMenu();
        }
        #endregion

        #region //Transitions
        protected virtual void NextMenu()
        {
            if(cantProgressForward) return;
            if(!nextMenu) return;
            sfxManager.Play(moveSFX);
            DeactivateMenu();
            scroller.ScrollNext();
            nextMenu.ActivateMenu();
        }

        protected virtual void PreviousMenu()
        {
            if(cantProgressBackward) return;
            if(!previousMenu) return;
            sfxManager.Play(moveSFX);
            DeactivateMenu();
            scroller.ScrollPrevious();
            previousMenu.ActivateMenu();
        }

        protected virtual void DeactivateMenu()
        {
            LockControl(true);
        }

        protected virtual void ActivateMenu()
        {
            LockControl(false);
        }
        #endregion
    }
}
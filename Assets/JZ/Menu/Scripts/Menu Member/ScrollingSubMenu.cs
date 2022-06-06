using UnityEngine;

namespace JZ.MENU
{
    public class ScrollingSubMenu : MenuMemberComponent
    {
        [SerializeField] MenuScroller scroller = null;
        [SerializeField] ScrollingMenu[] myMenus = null;

        protected override void OnHover(bool _isHovering)
        {
            if(_isHovering)
                HoverIn();
            else
                HoverOut();
        }

        void HoverOut()
        {
            foreach(ScrollingMenu mm in myMenus)
                mm.LockControl(true);
        }

        void HoverIn()
        {
            myMenus[scroller.menuNo].LockControl(false);
        }
    }
}

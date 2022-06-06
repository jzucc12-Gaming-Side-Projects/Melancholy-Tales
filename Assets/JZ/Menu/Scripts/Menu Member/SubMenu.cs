using UnityEngine;

namespace JZ.MENU
{
    public class SubMenu : MenuMemberComponent
    {
        [SerializeField] MonoBehaviour myMenu = null;


        protected override void OnHover(bool _isHovering)
        {
            if(_isHovering)
                HoverIn();
            else
                HoverOut();
        }

        void HoverOut()
        {
            myMenu.enabled = false;
        }

        void HoverIn()
        {
            myMenu.enabled = true;
        }
    }
}

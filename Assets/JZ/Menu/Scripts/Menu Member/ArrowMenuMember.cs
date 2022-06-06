using UnityEngine;
using UnityEngine.UI;

namespace JZ.MENU
{
    public class ArrowMenuMember : MenuMemberComponent
    {
        
        [SerializeField] Image arrowImage = null;
        Color arrowColor;

        protected override void Awake() 
        {
            base.Awake();
            arrowColor = arrowImage.color;
            OnHover(false);    

        }

        protected override void OnHover(bool _active)
        {
            Color newColor = _active ? arrowColor : Color.clear;
            arrowImage.color = newColor;
        }
    }
}
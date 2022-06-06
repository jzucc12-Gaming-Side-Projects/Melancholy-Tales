using UnityEngine;

namespace JZ.MENU
{
    public abstract class MenuMemberComponent : MonoBehaviour
    {
        MenuMember myMember;

        protected virtual void Awake() 
        {
            myMember = GetComponent<MenuMember>();    
        }

        protected virtual void OnEnable() 
        {
            myMember.OnHover += OnHover;
        }

        protected virtual void OnDisable() 
        {
            myMember.OnHover -= OnHover;
        }

        protected abstract void OnHover(bool _isHovering);
    }
}

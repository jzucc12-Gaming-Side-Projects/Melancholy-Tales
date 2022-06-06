using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JZ.MENU
{
    public class MenuMember : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler
    {
        public event Action<MenuMember> PointerEnter;
        public event Action<bool> OnHover;

        private void Start() 
        {
            Hover(false);    
        }

        public void Hover(bool _active) 
        { 
            OnHover?.Invoke(_active);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEnter?.Invoke(this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
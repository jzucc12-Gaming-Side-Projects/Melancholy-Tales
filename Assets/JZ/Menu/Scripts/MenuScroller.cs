using System.Collections;
using UnityEngine;

namespace JZ.MENU
{
    public class MenuScroller : MonoBehaviour
    {
        [SerializeField] bool scrollsHoizontally = true;
        [SerializeField] float scrollSpeed = 30f;
        IEnumerator scrollingRoutine;
        RectTransform myRect;
        float scrollPosition = 0;
        public int menuNo { get; private set; }


        #region //Monobehaviour
        private void Awake() 
        {
            menuNo = 0;
            myRect = GetComponent<RectTransform>();
        }
        #endregion

        #region //Positioning
        float GetScrollUnit() => scrollsHoizontally ? myRect.rect.width : myRect.rect.height;

        public void SetPosition(int _menuNo)
        {
            menuNo = _menuNo;
            scrollPosition = -_menuNo * GetScrollUnit();

            Vector2 newPosition = Vector2.zero;
            if(scrollsHoizontally) newPosition += new Vector2(scrollPosition, 0);
            else newPosition += new Vector2(0, scrollPosition);
            transform.localPosition = newPosition;
        }
        #endregion

        #region //Scrolling
        public void ActivateScroll(int _menuNo)
        {
            if(scrollingRoutine != null)
                StopCoroutine(scrollingRoutine);

            menuNo = _menuNo;
            scrollingRoutine = ScrollToMenu(_menuNo);
            StartCoroutine(scrollingRoutine);
        }

        public void ScrollNext()
        {
            ActivateScroll(menuNo + 1);
        }

        public void ScrollPrevious()
        {
            ActivateScroll(menuNo - 1);
        }

        IEnumerator ScrollToMenu(int _menuNo)
        {
            float target = -_menuNo * GetScrollUnit();

            while(scrollPosition != target)
            {
                scrollPosition = Mathf.MoveTowards(scrollPosition, target, scrollSpeed);

                Vector2 newPosition = Vector2.zero;
                if(scrollsHoizontally) newPosition += new Vector2(scrollPosition, 0);
                else newPosition += new Vector2(0, scrollPosition);

                transform.localPosition = newPosition;

                yield return null;
            }
        }
        #endregion
    }
}

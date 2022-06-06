using CFR.CARGO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CFR.UI
{
    public class ManifestListEntry : MonoBehaviour
    {
        #region//Cached variables
        [SerializeField] Image myImage = null;
        [SerializeField] Image arrow = null;
        [SerializeField] Image outline = null;
        [SerializeField] TextMeshProUGUI holdTypeDisplay = null;
        [SerializeField] Image[] holdTypeImages = null;
        Sprite noItemImage;
        Color arrowColor;
        #endregion


        #region//Monobehaviour
        private void Awake()
        {
            arrowColor = arrow.color;
            SetArrow(false);
            noItemImage = Resources.Load<Sprite>("Art/Cargo Item Art/NA");
        }
        #endregion

        #region//Entry setting
        public void UpdateEntry(CargoItem _item, HoldType _holdType, bool _showHold)
        {
            gameObject.SetActive(true);
            SetItem(_item);
            ChangeHoldDisplay(_holdType, _showHold);
        }

        void SetItem(CargoItem _item)
        {
            gameObject.SetActive(true);

            if (_item)
                myImage.sprite = _item.GetItemImage();
            else
                myImage.sprite = noItemImage;
        }

        void ChangeHoldDisplay(HoldType _holdType, bool _showHold)
        {
            //string displayText = "";
            holdTypeImages[0].enabled = false;
            holdTypeImages[1].enabled = false;

            if(_showHold)
            {
                if(_holdType == HoldType.conceal || _holdType == HoldType.SC)
                {
                    holdTypeImages[0].enabled = true;
                    holdTypeImages[0].sprite = Resources.Load<Sprite>("Art/Quirk Art/Concealed");
                    //displayText += "C";
                }

                // if(_holdType == HoldType.SC)
                //     displayText += ", ";

                if(_holdType == HoldType.small || _holdType == HoldType.SC)
                {
                    holdTypeImages[1].enabled = true;
                    holdTypeImages[1].sprite = Resources.Load<Sprite>("Art/Quirk Art/Small");
                    //displayText += "S";
                }
            }

            //holdTypeDisplay.text = displayText;
        }
        #endregion

        #region //Selection UI
        public void ChangeHighlightColor(Color _color)
        {
            outline.color = _color;
        }

        public void SetArrow(bool _enable)
        {
            arrow.enabled = _enable;
            Color newColor = _enable ? arrowColor : Color.clear;
            arrow.color = newColor;
        }

        public void Hide()
        {
            SetArrow(false);
            myImage.enabled = false;
            outline.enabled = false;
            holdTypeDisplay.enabled = false;
            enabled = false;
        }
        
        public void TurnOff()
        {
            gameObject.SetActive(false);
        }
        #endregion
    }
}
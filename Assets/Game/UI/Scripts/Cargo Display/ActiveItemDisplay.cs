using CFR.CARGO;
using JZ.MENU;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CFR.UI
{
    public class ActiveItemDisplay : MonoBehaviour
    {
        #region//Cached variables
        CargoItem activeItem;
        HoldType activeHoldType;
        Sprite noItemImage;
        #endregion

        #region//Display objects
        [Header("Display Objects")]
        [SerializeField] TextDisplay itemName = null;
        [SerializeField] Image itemImage = null;
        #endregion

        #region//Display lists
        [Header("Display Lists")]
        [SerializeField] TextMeshProUGUI attackerHeader = null;
        [SerializeField] TextMeshProUGUI loserHeader = null;
        [SerializeField] TextMeshProUGUI quirksHeader = null;
        [SerializeField] Image[] quirkImages = new Image[4];
        [SerializeField] Image[] typeImages = new Image[2];
        #endregion

        #region//Display options
        [Header("Display Options")]
        [SerializeField] bool displayTypes = true;
        [SerializeField] bool displayQuirks = true;
        #endregion


        #region//Monobehaviour
        private void Awake()
        {
            noItemImage = Resources.Load<Sprite>("Art/Cargo Item Art/NA");
        }

        private void Start()
        {
            if (!displayTypes) 
            {
                for(int ii = 0; ii < typeImages.Length; ii++)
                    typeImages[ii].enabled = false;

                attackerHeader.enabled = false;
                loserHeader.enabled = false;
            }

            if (!displayQuirks) 
            {
                for (int ii = 0; ii < quirkImages.Length; ii++)
                    quirkImages[ii].enabled = false;
            
                quirksHeader.enabled = false;
            }
        }

        private void OnEnable()
        {
            UpdateItem(null);
        }
        #endregion

        #region//Display methods
        public void UpdateItem(CargoItem _item)
        {
            activeItem = _item;

            if (activeItem == null)
            {
                itemName.SetText("Cargo Slot Empty");
                itemImage.sprite = noItemImage;
                if (displayTypes) UpdateTypeListImage(typeImages, false, null);
                if (displayQuirks) UpdateQuirkListImage(quirkImages, null);
            }
            else
            {
                itemName.SetText(activeItem.GetName());
                itemImage.sprite = activeItem.GetItemImage();
                if (displayTypes) UpdateTypeListImage(typeImages, false, activeItem);
                if (displayQuirks) UpdateQuirkListImage(quirkImages, activeItem);
            }
        }

        void UpdateTypeListImage(Image[] _list, bool _show, CargoItem _item)
        {
            for(int ii = 0; ii < _list.Length; ii++)
                _list[ii].enabled = false;

            attackerHeader.enabled = false;
            loserHeader.enabled = false;

            if(_item == null) return;

            attackerHeader.enabled = true;
            loserHeader.enabled = true;
            var attacker = _item.GetAttackerType();
            var loser = _item.GetLoserType();
            int showNum = 0;

            if(attacker)
            {
                attackerHeader.text = "Attacker:";
                _list[0].enabled = true;
                _list[0].sprite = attacker.GetIcon();
                showNum++;
            }
            else
                attackerHeader.text = "Attacker: None";
            
            if(loser)
            {
                loserHeader.text = "Loser:";
                _list[1].enabled = true;
                _list[1].sprite = loser.GetIcon();
                showNum++;
            }
            else
                loserHeader.text = "Loser: None";
        }

        void UpdateQuirkListImage(Image[] _list, CargoItem _item)
        {
            bool isSmall = true;

            for (int ii = 0; ii < _list.Length; ii++)
                _list[ii].enabled = false;
            
            quirksHeader.enabled = false;
            if(_item == null) return;

            int count = 0;

            foreach(Quirk quirk in _item.GetQuirks())
            {
                if(!quirk) continue;

                if(quirk.name == "Big")
                    isSmall = false;
                else
                {
                    _list[count].enabled = true;
                    _list[count].sprite = quirk.GetIcon();
                    count++;
                }
            }
            if(isSmall)
            {
                Sprite smallIcon = Resources.Load<Sprite>("Art/Quirk Art/Small");
                _list[count].enabled = true;
                _list[count].sprite = smallIcon; 
            }
            if(count > 0 || isSmall)
                quirksHeader.enabled = true;
        }
        #endregion
    }
}

// using CFR.INPUT;
// using CFR.SHIP;
// using UnityEngine;

// namespace CFR.UI
// {
//     public class DoubleManifestDisplayManager : ManifestDisplayManager
//     {
//         #region//Cached variables
//         [SerializeField] protected ActiveItemDisplay secondDisplay = null;
//         #endregion

//         #region//Second item variables
//         protected int secondItem = -99;
//         protected bool secondItemSet => secondItem != -99;
//         #endregion


//         #region//Monobehaviour
//         protected override void Start()
//         {
//             navigator = GetComponent<INavigationController>().GetNavigator();
//             listManager.SetManifest(myManifest);
//         }

//         protected override void OnEnable()
//         {
//             base.OnEnable();
//             secondDisplay.gameObject.SetActive(false);
//         }
//         #endregion

//         #region//Display methods
//         protected override void ItemFromNavigator()
//         {
//             if(navigator.activeItem == secondItem) return;

//             if(!navigator.activeItemSet)
//                 ResetItem();
//             else
//             {
//                 secondDisplay.gameObject.SetActive(true);
//                 SetItem(secondDisplay, navigator.activeItem, ref activeItem, false);
//             }
//         }

//         void ResetSecondItem()
//         {
//             secondDisplay.UpdateItem(null);
//             secondDisplay.gameObject.SetActive(false);
//             if (secondItemSet) listManager.UnhighlightListItem(secondItem);
//             secondItem = -99;
//         }

//         protected override void ToggleDisplay(ShipCargoState _newState)
//         {
//             if (_newState is TransferShipState)
//                 displayGO.SetActive(true);
//             else
//                 displayGO.SetActive(false);
//         }
//         #endregion
//     }
// }
// using UnityEngine;

// namespace CFR.INPUT
// {
//     public class SingleManifestNavigator : ManifestNavigator
//     {
//         #region//Constructor
//         public SingleManifestNavigator(PlayerInputManager _im, bool _forShip, int _maxSize) : base(_im, _forShip, _maxSize) { }
//         #endregion

//         #region//Shutdown
//         public override void Shutdown()
//         {
//             ResetItem();
//             base.Shutdown();
//         }
//         #endregion

//         #region//Item methods
//         public override void Selection(int _itemNo)
//         {
//             if (_itemNo != activeItem)
//                 SetItem(_itemNo);
//             else
//                 ResetItem();
//         }

//         protected override void SetItem(int _itemNo, bool _ = true)
//         {
//             activeItem = _itemNo;
//         }

//         public override void ResetItem(bool _ = true)
//         {
//             if (activeItemSet)
//                 activeItem = -99;

//             if (!GameSettings.menuScheme) arrow = 0;
//         }
//         #endregion
//     }
// }
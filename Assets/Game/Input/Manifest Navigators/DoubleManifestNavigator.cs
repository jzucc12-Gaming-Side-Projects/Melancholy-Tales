// namespace CFR.INPUT
// {
//     public class DoubleManifestNavigator : ManifestNavigator
//     {
//         public int secondItem { get; private set; }
//         public bool secondItemSet => secondItem != -99;


//         #region//Constructor
//         public DoubleManifestNavigator(PlayerInputManager _im, bool _forShip, int _maxSize, int _shipSize = 0) : base(_im, _forShip, _maxSize, _shipSize)
//         {
//             secondItem = -99;
//         }
//         #endregion

//         #region//Shutdown
//         public override void Shutdown()
//         {
//             ResetItem(true);
//             ResetItem(false);
//             base.Shutdown();
//         }
//         #endregion


//         #region//Item Methods
//         public override void Selection(int _itemNo)
//         {
//             if (_itemNo == activeItem)
//                 ResetItem(true);
//             else
//                 SetItem(_itemNo, true);
//         }

//         protected override void SetItem(int _itemNo, bool _firstItem)
//         {
//             if (_firstItem)
//                 activeItem = _itemNo;
//             else
//                 secondItem = _itemNo;
//         }

//         public override void ResetItem(bool _firstItem)
//         {
//             if (_firstItem)
//             {
//                 if (!activeItemSet) return;
//                 activeItem = -99;
//             }
//             else
//             {
//                 if (!secondItemSet) return;
//                 secondItem = -99;
//             }

//             if (!GameSettings.menuScheme) arrow = 0;
//         }
//         #endregion
//     }

// }
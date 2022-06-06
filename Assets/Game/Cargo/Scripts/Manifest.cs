using System;
using System.Collections.Generic;
using System.Linq;
using CFR.ITINERARY;
using UnityEngine;

namespace CFR.CARGO
{
    public class Manifest : MonoBehaviour, IItineraryElement
    {
        #region//Manifest variables
        [SerializeField] CargoItem[] cargoList = new CargoItem[6];
        [SerializeField] HoldType[] holdTypes = new HoldType[6];
        int maxSize => cargoList.Length;
        public static event Action<Manifest, CargoItem, CargoItem> manifestConflict;
        #endregion

        #region //Saved states
        public event Action ItineraryChanged;
        List<CargoItem[]> savedStates = new List<CargoItem[]>();
        CargoItem[] heldState = new CargoItem[0];
        #endregion


        #region//Getters
        public IEnumerable<CargoItem> GetItems() { return cargoList; }
        public CargoItem GetItem(int _id) { return cargoList[_id]; }
        public int GetMaxSize() { return maxSize; }
        public HoldType GetHoldType(int _id) { return holdTypes[_id]; }
        #endregion

        #region//End game checking
        public bool ManifestCheck()
        {
            bool hasObservant = HasQuirk("Observant");
            for (int ii = 0; ii < cargoList.Length; ii++)
            {
                CargoItem firstItem = cargoList[ii];
                if (!firstItem) continue;

                for (int jj = ii + 1; jj < cargoList.Length; jj++)
                {
                    CargoItem secondItem = cargoList[jj];
                    if (!secondItem) continue;

                    bool firstAttacker = firstItem.DefeatCheck(secondItem, hasObservant);
                    bool secondAttacker = false;
                    if(!firstAttacker) secondAttacker = secondItem.DefeatCheck(firstItem, hasObservant);

                    if (firstAttacker && HandleDefeat(firstItem, secondItem)) return true;
                    if (secondAttacker && HandleDefeat(secondItem, firstItem)) return true;
                }
            }
            return false;
        }

        private bool HandleDefeat(CargoItem _attackerItem, CargoItem _loserItem)
        {
            //Check to see if defeat is cancelled
            foreach(Quirk quirk in _attackerItem.GetQuirks())
            {
                if(!quirk.IsManifestCheck()) continue;
                ComparisonQuirk cq = (ComparisonQuirk)quirk;
                if(!cq) continue;
                if(cq.ComparisonCheck(_attackerItem, this)) return false;
            }

            manifestConflict?.Invoke(this, _attackerItem, _loserItem);
            return true;
        }
        #endregion

        #region //Manifest searching
        public int GetItemsInManifest()
        {
            int count = 0;
            foreach (CargoItem _item in cargoList)
            {
                if (_item == null) continue;
                count++;
            }
            return count;
        }

        public bool IsManifestFull()
        {
            return GetItemsInManifest() == maxSize;
        }

        public int GetTypeCount(AttackerType _type)
        {
            return cargoList.ToList().FindAll(x =>
            {
                if (x)
                    return x.HasAttackerType(_type);
                else
                    return false;
            }).Count;
        }

        public int GetTypeCount(LoserType _type)
        {
            return cargoList.ToList().FindAll(x =>
            {
                if (x)
                    return x.HasLoserType(_type);
                else
                    return false;
            }).Count;
        }

        public CargoItem HasQuirk(Quirk _quirk, bool _ignoreIfSneaky)
        {
            foreach(CargoItem item in cargoList)
            {
                if(!item) continue;
                if(_ignoreIfSneaky && item.IsSneaky()) continue;
                if(item.HasQuirk(_quirk)) return item;
            }
            return null;
        }

        public CargoItem HasQuirk(string _quirkName)
        {
            foreach(CargoItem item in cargoList)
            {
                if(!item) continue;
                if(item.HasQuirk(_quirkName)) return item;
            }
            return null;
        }
        #endregion

        #region//Manifest modification
        public bool TrySwap(CargoItem _item, int _holdNo)
        {
            if(_item == null) return true;
            HoldType holdType = holdTypes[_holdNo];

            foreach(Quirk quirk in _item.GetQuirks())
            {
                if(!quirk.IsSwapCheck()) continue;
                HoldQuirk hq = (HoldQuirk)quirk;
                if(!hq) continue;
                if(!hq.HoldCheck(holdType)) return false;
            }
            return true;
        }

        public void SwapItems(int _myItem, int _theirItem, Manifest _theirManifest)
        {
            CargoItem temp = cargoList[_myItem];
            cargoList[_myItem] = _theirManifest.cargoList[_theirItem];
            _theirManifest.cargoList[_theirItem] = temp;
        }

        public void CopyManifest(Manifest _copyFrom)
        {
            cargoList = new CargoItem[_copyFrom.cargoList.Length];
            holdTypes = new HoldType[_copyFrom.cargoList.Length];
            cargoList = _copyFrom.cargoList;
            holdTypes = _copyFrom.holdTypes;
        }
        #endregion

        #region //IItinerary Element
        public void SaveState()
        {
            if(tag == "LZDisplay") return;
            var newState = new CargoItem[cargoList.Length];
            cargoList.CopyTo(newState,0);
            savedStates.Add(newState);
        }

        public void HoldCurrentState()
        {
            if(tag == "LZDisplay") return;
            heldState = new CargoItem[cargoList.Length];
            cargoList.CopyTo(heldState,0);
            savedStates.Add(heldState);
        }

        public void RestoreCurrentState()
        {
            if(tag == "LZDisplay") return;
            heldState.CopyTo(cargoList,0);
            savedStates.RemoveAt(savedStates.Count - 1);
            heldState = new CargoItem[0];
            ItineraryChanged?.Invoke();
        }

        public void ShowState(int _index)
        {
            if(tag == "LZDisplay") return;
            savedStates[_index].CopyTo(cargoList,0);
            ItineraryChanged?.Invoke();
        }

        public void DeleteOldStates(int _limit)
        {
            if(tag == "LZDisplay") return;
            savedStates.RemoveRange(_limit + 1, savedStates.Count - _limit - 2);
        }

        public void RevertHeldState(int _index)
        {
            if(tag == "LZDisplay") return;
            savedStates[_index].CopyTo(heldState,0);
        }
        #endregion
    }
}

public enum HoldType
{
    normal = 0,
    conceal = 1,
    small = 2,
    SC = 3
}

using CFR.CARGO;
using CFR.LZ;
using CFR.SHIP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CFR.UI
{
    public class TypeHelpEntryUI : MonoBehaviour
    {
        #region //Display Info
        [Header("Display Info")]
        [SerializeField] Attacker myAttacker = Attacker.hunter;
        AttackerType attacker = null;
        LoserType loser = null;
        ActiveLZController lZController;
        Manifest myManifest;
        Color activeQuirkColor = Color.white;
        Color inactiveQuirkColor = Color.gray;
        bool carefulMet;
        bool honorMet;
        bool loneWolfMet;
        int numAttackers = 0;
        int numLosers = 0;

        #endregion

        #region //UI Components
        [Header("UI Components")]
        [SerializeField] TextMeshProUGUI attackerCount = null;
        [SerializeField] TextMeshProUGUI loserCount = null;
        [SerializeField] Image carefulImage = null;
        [SerializeField] Image honorImage = null;
        [SerializeField] Image loneWolfImage = null;
        [SerializeField] Transform carefulContainer = null;
        [SerializeField] Transform honorContainer = null;
        [SerializeField] Transform loneWolfContainer = null;
        [SerializeField] Transform resultContainer = null;
        #endregion


        #region //Monobehaviour
        private void Awake()
        {
            myManifest = GetComponentInParent<Manifest>();
            lZController = FindObjectOfType<ActiveLZController>();
            if(IsHunter())
            {
                attacker = Resources.Load<AttackerType>("Cargo Types/Hunter");
                loser = Resources.Load<LoserType>("Cargo Types/Wanted");
            }
            else if(IsThief())
            {
                attacker = Resources.Load<AttackerType>("Cargo Types/Thief");
                loser = Resources.Load<LoserType>("Cargo Types/Valuables");
            }
            else
            {
                attacker = Resources.Load<AttackerType>("Cargo Types/Renegade");
                loser = Resources.Load<LoserType>("Cargo Types/Wanted");
            }
        }

        private void OnEnable()
        {
            CargoTransferer.OnSwap += RefreshUI;
            lZController.LZControllerStateChange += Activated;
            RefreshUI();
        }

        private void OnDisable()
        {
            CargoTransferer.OnSwap -= RefreshUI;
            lZController.LZControllerStateChange -= Activated;
        }
        #endregion

        #region //Attacker Check
        private bool IsHunter() { return myAttacker == Attacker.hunter; }
        private bool IsThief() { return myAttacker == Attacker.thief; }
        private bool IsRenegade() { return myAttacker == Attacker.renegade; }
        #endregion

        #region //Refresh UI
        private void Activated(bool _)
        {
            RefreshUI();
        }

        private void RefreshUI()
        {
            if(myManifest == null) return;

            CountTypes();
            if(IsHunter()) UpdateCareful();
            if(!IsRenegade()) UpdateHonor();
            if(IsRenegade()) UpdateLoneWolf();
            UpdateResult();
        }

        private void CountTypes()
        {
            numAttackers = myManifest.GetTypeCount(attacker);
            numLosers = myManifest.GetTypeCount(loser);
            if(IsRenegade()) numLosers--;
            attackerCount.text = numAttackers.ToString();
            loserCount.text = numLosers.ToString();
        }

        private void UpdateCareful()
        {
            bool allCareful = CheckForAllHaveQuirk("Careful");
            carefulMet = allCareful && numAttackers < numLosers;

            SetCheckOrX(carefulContainer, carefulMet, !allCareful);
            Color imageColor = carefulMet ? activeQuirkColor : inactiveQuirkColor;
            carefulImage.color = imageColor;
            carefulImage.transform.GetChild(0).GetComponent<Image>().color = imageColor;
            carefulImage.transform.GetChild(1).GetComponent<Image>().color = imageColor;
        }

        private void UpdateHonor()
        {
            bool allHonorable = CheckForAllHaveQuirk("Honorable");
            honorMet = allHonorable && numAttackers > numLosers;

            SetCheckOrX(honorContainer, honorMet, !allHonorable);
            Color imageColor = honorMet ? activeQuirkColor : inactiveQuirkColor;
            honorImage.color = imageColor;
            honorImage.transform.GetChild(0).GetComponent<Image>().color = imageColor;
            honorImage.transform.GetChild(1).GetComponent<Image>().color = imageColor;
        }

        private void UpdateLoneWolf()
        {
            bool allLoneWolf = CheckForAllHaveQuirk("Lone Wolf");
            loneWolfMet = allLoneWolf && numLosers > 1;

            SetCheckOrX(loneWolfContainer, loneWolfMet, !allLoneWolf);
            Color imageColor = loneWolfMet ? activeQuirkColor : inactiveQuirkColor;
            loneWolfImage.color = imageColor;
            honorImage.transform.GetChild(0).GetComponent<Image>().color = imageColor;
            honorImage.transform.GetChild(1).GetComponent<Image>().color = imageColor;
        }

        private void UpdateResult()
        {
            bool useCheck = numAttackers == 0 || numLosers == 0;
            useCheck = useCheck || carefulMet;
            useCheck = useCheck || honorMet;
            useCheck = useCheck || loneWolfMet;
            SetCheckOrX(resultContainer, useCheck);
        }

        private bool CheckForAllHaveQuirk(string _quirkName)
        {
            if(numAttackers == 0) return false;

            foreach(var item in myManifest.GetItems())
            {
                if(item == null) continue;
                if(!item.HasAttackerType(attacker)) continue;
                if(!item.HasQuirk(_quirkName)) return false;
            }

            return true;
        }

        private void SetCheckOrX(Transform _container, bool _useCheck, bool _useNA = false)
        {
            _container.GetChild(0).gameObject.SetActive(_useCheck);
            _container.GetChild(1).gameObject.SetActive(!_useCheck && !_useNA);
            _container.GetChild(2).gameObject.SetActive(!_useCheck && _useNA);
        }
        #endregion
    }

    enum Attacker
    {
        hunter = 0,
        thief = 1,
        renegade = 2
    }
}
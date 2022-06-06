using CFR.CARGO;
using UnityEngine;
using UnityEngine.UI;

namespace CFR.UI
{
    public class LZItineraryDisplay : MonoBehaviour
    {
        #region //Components
        [SerializeField] private RectTransform backgroundBox = null;
        private GridLayoutGroup grid;
        private Manifest myManifest;
        private Canvas myCanvas;
        private ManifestListEntry[] entries;
        #endregion

        #region//Variables
        [SerializeField] private bool showAbove = true;
        [SerializeField] private bool showHoldType = true;
        [SerializeField] private float yOffset = 3f;
        [SerializeField] private float fullBoxOffset = -0.2f;
        [SerializeField] private float halfBoxOffset = 1.3f;
        #endregion


        #region //Monobehaviour
        private void Awake()
        {
            myManifest = GetComponentInParent<Manifest>();
            grid = GetComponentInChildren<GridLayoutGroup>();
            entries = GetComponentsInChildren<ManifestListEntry>();
            myCanvas = GetComponent<Canvas>();
        }

        private void Start()
        {
            Show(false);  
        }

        private void OnEnable()
        {
            myManifest.ItineraryChanged += UpdateList;
        }

        private void OnDisable()
        {
            myManifest.ItineraryChanged -= UpdateList;
        }
        #endregion

        #region //Display variables
        public void Show(bool _show)
        {
            myCanvas.enabled = _show;
        }

        public void UpdateList()
        {
            OrientList();

            for(int ii = 0; ii < entries.Length; ii++)
            {
                if(ii < myManifest.GetMaxSize())
                {
                    entries[ii].UpdateEntry(myManifest.GetItem(ii), 
                                            myManifest.GetHoldType(ii), showHoldType);
                }
                else
                    entries[ii].TurnOff();
            }
        }

        private void OrientList()
        {
            float currentOffset = yOffset * (showAbove ? 1 : -1);
            bool fullBox = myManifest.GetMaxSize() > Globals.maxLZHold/2; 
            if(showAbove) currentOffset  *= (fullBox ? 1 : 0.5f);
            transform.localPosition = new Vector2(transform.localPosition.x, currentOffset);

            float bottomVal = (fullBox ? fullBoxOffset : halfBoxOffset);
            backgroundBox.offsetMin = new Vector2(backgroundBox.offsetMin.x, bottomVal);
            if(!fullBox)
                grid.constraintCount = myManifest.GetMaxSize();
        }
        #endregion
    }
}

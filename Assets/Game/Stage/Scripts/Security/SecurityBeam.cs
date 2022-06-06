using System;
using CFR.CARGO;
using UnityEngine;
using UnityEngine.UI;

namespace CFR.STAGE
{
    public class SecurityBeam : MonoBehaviour
    {
        [SerializeField] GameObject parentObject = null;
        Manifest shipManifest;
        public static event Action<CargoItem> alarmTripped;
        Color startColor;
        Color alertedColor = new Color(1,0.25f,0.25f);
        bool tripped = false;
        int framesBetweenChecks = 10;
        int currentFrameCount = 0;


        #region //Monobehaviour
        private void Awake() 
        {
            shipManifest = GameObject.FindGameObjectWithTag("ShipManifest").GetComponent<Manifest>();
        }

        private void Start()
        {
            if(GetComponent<Image>()) startColor = GetComponent<Image>().color;
            else startColor = GetComponent<SpriteRenderer>().color;
        }

        private void OnTriggerEnter2D(Collider2D other) 
        {
            currentFrameCount = 0;
            DetermineCheckTarget(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if(++currentFrameCount % framesBetweenChecks != 0) return;
            currentFrameCount = 0;

            DetermineCheckTarget(other);
        }

        private void OnTriggerExit2D(Collider2D other) 
        {
            DetermineCheckTarget(other);
        }
        #endregion

        #region //Illegal check
        private void DetermineCheckTarget(Collider2D other)
        {
            if(tripped) return;

            if(other.tag == "Player")
                IllegalCheck(shipManifest);
            else
            {
                Manifest manifest = other.GetComponent<Manifest>();
                if (manifest == null) return;
                IllegalCheck(manifest);
            }
        }

        private void IllegalCheck(Manifest _manifest)
        {
            if (!_manifest.HasQuirk("Illegal")) return;
            int count = -1;
            bool sneakyFound = false;

            foreach (CargoItem item in _manifest.GetItems())
            {
                count++;
                if (!item) continue;

                foreach (Quirk quirk in item.GetQuirks())
                {
                    if (!quirk.IsSecurityCheck()) continue;
                    HoldQuirk hq = (HoldQuirk)quirk;
                    if (!hq) continue;
                    if (hq.HoldCheck(_manifest.GetHoldType(count))) continue;
                    if (!sneakyFound && item.IsSneaky())
                    {
                        sneakyFound = true;
                        continue;
                    }

                    HandleSecurityTrip(item);
                }
            }
        }

        void HandleSecurityTrip(CargoItem _item)
        {
            foreach(SecurityBeam beam in parentObject.GetComponentsInChildren<SecurityBeam>())
            {
                if(beam.GetComponent<Image>()) beam.GetComponent<Image>().color = alertedColor;
                else beam.GetComponent<SpriteRenderer>().color = alertedColor;
                beam.tripped = true;
            }

            SecurityBeamMover mover = parentObject.GetComponent<SecurityBeamMover>();
            if(mover != null)
                mover.StopAllCoroutines();

            alarmTripped?.Invoke(_item);
        }

        public void SetTrippedBeam(bool _tripped)
        {
            if(!tripped) return;
            Color beamColor = (_tripped ? alertedColor : startColor);

            if(GetComponent<Image>()) GetComponent<Image>().color = beamColor;
            else GetComponent<SpriteRenderer>().color = beamColor;
        }

        public void ResetTripped()
        {
            tripped = false;
        }
        #endregion
    }
}

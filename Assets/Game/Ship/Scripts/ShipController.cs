using System.Collections.Generic;
using CFR.ITINERARY;
using CFR.LZ;
using JZ.AUDIO;
using UnityEngine;

namespace CFR.SHIP
{
    public class ShipController : MonoBehaviour, IItineraryElement
    {
        #region//Cached variables
        public Rigidbody2D myRB { get; private set; }
        [SerializeField] GameObject body = null;
        public ActiveLZController LZController { get; private set; }
        public AudioManager sfxManager { get; private set; }
        #endregion

        #region//Movement variables
        [SerializeField] float moveSpeed = 0.1f;
        public float shipSpeed { get { return moveSpeed; } private set { moveSpeed = value; } }
        public Vector2 movementVector { get; private set; }
        #endregion

        #region//Landing variables
        [SerializeField] LayerMask landingLayer = -1;
        public LandingZone currentLZ { get; private set; }
        public bool overLandingZone => currentLZ != null;
        #endregion

        #region//Saved States
        List<ShipItineraryState> savedStates = new List<ShipItineraryState>();
        ShipItineraryState heldState = new ShipItineraryState();
        #endregion

        #region//Monobehaviour
        private void Awake()
        {
            currentLZ = null;
            myRB = GetComponent<Rigidbody2D>();
            LZController = FindObjectOfType<ActiveLZController>();
            movementVector = new Vector2(0, 0);
            sfxManager = GetComponent<AudioManager>();
        }

        private void FixedUpdate()
        {
            Vector2 newPos = (Vector2)transform.position + movementVector * moveSpeed;
            myRB.MovePosition(newPos);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (landingLayer.value == 1 << collision.gameObject.layer)
            {
                currentLZ = collision.GetComponent<LandingZone>();
                LZController.StartUp(currentLZ);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (landingLayer.value == 1 << collision.gameObject.layer)
            {
                currentLZ = null;
                LZController.ShutDown();
            }
        }
        #endregion

        public GameObject GetBody() { return body; }

        #region//Movement methods
        public void SetMovementVector(Vector2 _vector)
        {
            movementVector = _vector;
        }

        public void SetRotation(Vector2 _movementVector)
        {
            float angle = Vector2.SignedAngle(Vector2.down, _movementVector);
            body.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        #endregion

        #region//Itinerary Element
        public void SaveState()
        {
            var newState = new ShipItineraryState();
            newState.shipPosition = transform.position;
            newState.shipRotation = body.transform.rotation;
            savedStates.Add(newState);
        }

        public void HoldCurrentState()
        {
            heldState.shipPosition = transform.position;
            heldState.shipRotation = body.transform.rotation;
            savedStates.Add(heldState);
        }

        public void RestoreCurrentState()
        {
            transform.position = heldState.shipPosition;
            body.transform.rotation = heldState.shipRotation;
            savedStates.RemoveAt(savedStates.Count - 1);
            heldState = new ShipItineraryState();
        }

        public void ShowState(int _index)
        {
            var showedState = savedStates[_index];
            transform.position = showedState.shipPosition;
            body.transform.rotation = showedState.shipRotation;
        }

        public void DeleteOldStates(int _limit)
        {
            savedStates.RemoveRange(_limit + 1, savedStates.Count - _limit - 2);
        }

        public void RevertHeldState(int _index)
        {
            heldState = new ShipItineraryState(savedStates[_index]);
        }

        private struct ShipItineraryState
        {
            public Vector3 shipPosition;
            public Quaternion shipRotation;

            public ShipItineraryState(ShipItineraryState _copyState)
            {
                shipPosition = _copyState.shipPosition;
                shipRotation = _copyState.shipRotation;
            }
        }
        #endregion
    }

    public enum SM
    {
        movement = 0,
        cargo = 1
    }
}
using CFR.INPUT;
using JZ.CORE;
using System.Collections;
using UnityEngine;

namespace CFR.SHIP
{
    public class LandingState : ShipMoveState
    {
        #region//State Methods
        public LandingState(ShipMoveSM _sm, PlayerInputManager _im, ShipController _controller) : base(_sm, _im, _controller) { }

        public override void StartState()
        {
            controller.sfxManager.Stop("Take Off");
            controller.sfxManager.Play("Land");
            inputManager.landedMenuSystem.Activate(true);
            ShipStartUp();
        }

        public override void EndState(State nextState)
        {
            inputManager.landedMenuSystem.Activate(false);
        }

        public override void StateUpdate()
        {
            StateChangeCheck();
        }

        protected override void StateChangeCheck()
        {
            if (!StateChangePreCheck()) return;
            TakeOffCheck();
        }
        #endregion

        #region//StartUp/ShutDown
        private void ShipStartUp()
        {
            sm.StartCoroutine(LandingSequence(controller.currentLZ.transform.position));
            controller.SetMovementVector(new Vector2(0, 0));
        }
        #endregion

        #region//Take-off and landing
        IEnumerator LandingSequence(Vector3 _landingLocation)
        {
            sm.lockState = true;
            float x = _landingLocation.x - sm.transform.position.x;
            float y = _landingLocation.y - sm.transform.position.y;
            Vector2 move = new Vector2(x, y).normalized;
            controller.SetRotation(move);

            while (sm.transform.position != _landingLocation)
            {
                yield return null;
                controller.transform.position = Vector3.MoveTowards(sm.transform.position, _landingLocation, controller.shipSpeed / 4);
            }

            controller.SetRotation(Vector2.right);
            sm.lockState = false;
            controller.SetMovementVector(new Vector2(0, 0));
        }

        void TakeOffCheck()
        {
            if (!inputManager.shipSystem.landInput) return;
            inputManager.shipSystem.ExpendLandInput();
            sm.ChangeState(sm.movingState);
        }
        #endregion
    }
}
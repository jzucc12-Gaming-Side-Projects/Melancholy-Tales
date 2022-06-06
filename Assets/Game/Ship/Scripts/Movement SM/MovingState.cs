using System.Linq;
using CFR.INPUT;
using JZ.CORE;
using UnityEngine;

namespace CFR.SHIP
{
    public class MovingState : ShipMoveState
    {
        SpriteRenderer[] flameRenderers;
        bool flying = false;
        float fadeOutTime = 0.3f;

        #region//State Methods
        public MovingState(ShipMoveSM _sm, PlayerInputManager _im, ShipController _controller) : base(_sm, _im, _controller) 
        { 
            var renderers = controller.GetBody().GetComponentsInChildren<SpriteRenderer>();
            flameRenderers = renderers.Where(x => x.gameObject != controller.GetBody().gameObject).ToArray();
            StopFlying();
        }

        public override void StateUpdate()
        {
            Vector2 input = inputManager.shipSystem.movementAction.ReadValue<Vector2>();
            MoveFromInput(input);

            if(input != Vector2.zero)
            {
                RotateFromInput(input);
                if(!flying) StartFlying();
            }
            else if (flying)
                StopFlying();

            StateChangeCheck();
        }

        public override void StartState()
        {
            controller.SetMovementVector(new Vector2(0, 0));
            if(sm.previousState is LandingState)
            {
                controller.sfxManager.Stop("Land");
                controller.sfxManager.Play("Take Off");
            }
        }

        public override void EndState(State nextState)
        {
            controller.sfxManager.FadeOut("Flying", fadeOutTime);
            StopFlying();
        }

        protected override void StateChangeCheck()
        {
            if (!StateChangePreCheck()) return;
            LandCheck();
        }
        #endregion

        #region//Movement methods
        void MoveFromInput(Vector2 _input)
        {  
            controller.SetMovementVector(_input);
        }

        void RotateFromInput(Vector2 _input)
        {
            controller.SetRotation(_input);
        }
        
        void LandCheck()
        {
            if (!inputManager.shipSystem.landInput) return;
            inputManager.shipSystem.ExpendLandInput();
            
            if (!controller.overLandingZone) return;
            sm.ChangeState(sm.landState);
        }
        #endregion

        private void StartFlying()
        {
            flying = true;
            controller.sfxManager.Play("Flying");
            foreach(SpriteRenderer renderer in flameRenderers)
                renderer.enabled = true;

        }

        private void StopFlying()
        {
            flying = false;
            controller.sfxManager.FadeOut("Flying", fadeOutTime);
            foreach(SpriteRenderer renderer in flameRenderers)
                renderer.enabled = false;
        }
    }
}

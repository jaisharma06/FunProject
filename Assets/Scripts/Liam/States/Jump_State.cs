using FunProject.Characters.Common;
using System;

namespace FunProject.Characters.Liam.States
{
    class Jump_State : State
    {
        private LiamController _liamController;

        public Jump_State(ICharacter character, string animationState) : base(character, animationState)
        {
            _liamController = character as LiamController;
        }

        public override void OnAnimationEnd()
        {
            base.OnAnimationEnd();
            _liamController.stateMachine.SwitchToNewState(typeof(InAir_State));
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _liamController.rb.AddForce(UnityEngine.Vector3.up * _liamController._data.jumpForce, UnityEngine.ForceMode.VelocityChange);
        }

        public override void OnExit()
        {
            base.OnExit();
            _liamController.isJumping = false;
        }

        public override Type Tick()
        {
            return typeof(Jump_State);
        }
    }
}

using FunProject.Characters.Common;
using System;

namespace FunProject.Characters.Liam.States
{
    class Land_State : State
    {
        protected LiamController _liamController;

        public Land_State(ICharacter character, string animationState) : base(character, animationState)
        {
            _liamController = character as LiamController;
        }

        public override void OnAnimationEnd()
        {
            base.OnAnimationEnd();
            _liamController.stateMachine.SwitchToNewState(typeof(Idle_State));
        }

        public override Type Tick()
        {
            return typeof(Land_State);
        }
    }
}

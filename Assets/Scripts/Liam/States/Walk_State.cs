using FunProject.Characters.Common;
using System;

namespace FunProject.Characters.Liam.States
{
    public class Walk_State : State
    {
        private LiamController _liamController;

        public Walk_State(ICharacter character, string animationState) : base(character, animationState)
        {
            _liamController = character as LiamController;
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override Type Tick()
        {
            if (_liamController.isJumping) {
                return typeof(Jump_State);
            }

            if (_liamController.isGrounded) {
                _character.UpdateSpeed();
                _liamController.UpdateAxisData();
                if (_character.speed <= 0) {
                    return typeof(Idle_State);
                } else if (_character.speed <= _liamController._data.walkSpeed) {
                    return typeof(Walk_State);
                } else {
                    return typeof(Run_State);
                }
            } else {
                return typeof(InAir_State);
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}

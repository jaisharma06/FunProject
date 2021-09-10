using FunProject.Characters.Common;
using System;
using System.Collections;
using UnityEngine;

namespace FunProject.Characters.Liam.States
{
    public class InAir_State : State
    {
        protected LiamController _liamController;
        public InAir_State(ICharacter character, string animationState) : base(character, animationState)
        {
            _liamController = character as LiamController;
        } 

        public override Type Tick()
        {
            if (_liamController.isGrounded) {
                return typeof(Idle_State);
            } else {
                return typeof(InAir_State);
            }
        }
    }
}
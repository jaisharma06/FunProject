using System;
using UnityEngine;

namespace FunProject.Characters.Common
{
    public abstract class State
    {
        protected ICharacter _character;
        protected string _animationState;
        public State(ICharacter character, string animationState)
        {
            _character = character;
            _animationState = animationState;
        }

        public abstract Type Tick();
        public virtual void OnEnter() 
        {
            if (!string.IsNullOrEmpty(_animationState)) {
                _character._animator.SetBool(_animationState, true);
            }
        }
        public virtual void OnExit() 
        {
            if (!string.IsNullOrEmpty(_animationState)) {
                _character._animator.SetBool(_animationState, false);
            }
        }
        public virtual void ReceiveContext(UnityEngine.Object context) { }

        public virtual void OnAnimationEnd() { }
    }
}

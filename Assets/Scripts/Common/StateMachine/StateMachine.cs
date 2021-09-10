using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FunProject.Characters.Common
{
    public class StateMachine : MonoBehaviour
    {
        private Dictionary<Type, State> _availableStates;
        public State pCurrentState { get; private set; }
        public event Action<State> onStateChangedEvent;

        public void SetStates(Dictionary<Type, State> states)
        {
            _availableStates = states;
        }

        private void Update()
        {
            if (pCurrentState == null) {
                pCurrentState = _availableStates.Values.First();
                pCurrentState.OnEnter();
            }

            var nextState = pCurrentState?.Tick();

            if (nextState != null && nextState != pCurrentState.GetType()) {
                SwitchToNewState(nextState);
            }
        }

        public void SwitchToNewState(Type nextState)
        {
            pCurrentState?.OnExit();
            pCurrentState = _availableStates[nextState];
            pCurrentState?.OnEnter();
            onStateChangedEvent?.Invoke(pCurrentState);
        }

        public void OnAnimationEnd()
        {
            if (pCurrentState != null) {
                pCurrentState.OnAnimationEnd();
            }
        }
    }
}
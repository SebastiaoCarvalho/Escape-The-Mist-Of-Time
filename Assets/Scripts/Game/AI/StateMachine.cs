using UnityEngine;

namespace Escape.Game.AI {

    public class StateMachine {

        private State[] _states;
        public State CurrentState { get; private set; }
        private State _initialState;

        public StateMachine() {
            
        }

        public virtual void Build(State[] states, State initialState) {
            _states = states;
            _initialState = initialState;
            Reset();
        }

        public void ChangeState(State newState) {
            CurrentState?.Exit();

            CurrentState = newState;
            CurrentState.Enter();
        }

        public void ExecuteStateUpdate() {
            Update();
            CurrentState?.Execute();
        }

        public void Update() {
            foreach (Transition transition in CurrentState.Transitions) {
                if (transition.IsTriggered()) {
                    ChangeState(transition.TargetState);
                    Debug.Log("Transitioned to " + CurrentState.GetType().Name);
                    break;
                }
            }
        }

        public void Reset() { // ignore exit from previous state and enter to new state
            CurrentState = _initialState;
            CurrentState.Enter();
        }

    }

}
using Sources.Infrastructure.StateMachines.States;
using UnityEngine;

namespace Sources.Infrastructure.StateMachines
{
    public abstract class FiniteStateMachine
    {
        private FiniteState _current;

        public void Start(FiniteState startState)
        {
            MoveNextState(startState);
        }

        public void Update()
        {
            _current.Update();
            
            if (_current.TryGetNextState(out FiniteState state) == false)
            {
                return;
            }

            MoveNextState(state);
        }

        private void MoveNextState(FiniteState nextState)
        {
            _current?.Exit();
            _current = nextState;
            _current.Enter();
        }
    }
}
using Sources.Infrastructure.StateMachines.FiniteStateMachines.States;
using UnityEngine;

namespace Sources.Infrastructure.StateMachines.FiniteStateMachines
{
    public abstract class FiniteStateMachine
    {
        private FiniteState _current;

        public void Start(FiniteState startState)
        {
            MoveNextState(startState);
        }

        public void Stop() => 
            _current.Exit();

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
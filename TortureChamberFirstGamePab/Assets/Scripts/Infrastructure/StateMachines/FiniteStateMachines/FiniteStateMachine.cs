using Scripts.Infrastructure.StateMachines.FiniteStateMachines.States;

namespace Scripts.Infrastructure.StateMachines.FiniteStateMachines
{
    public abstract class FiniteStateMachine
    {
        private FiniteState _current;

        protected void Start(FiniteState startState) =>
            MoveNextState(startState);

        protected void Stop() =>
            _current.Exit();

        protected void Update()
        {
            _current.Update();

            if (_current.TryGetNextState(out var state) == false) return;

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
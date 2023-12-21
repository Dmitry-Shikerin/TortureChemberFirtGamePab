using System;
using Sources.Infrastructure.StateMachines.States;

namespace Sources.Infrastructure.StateMachines.Transitions
{
    public abstract class FiniteTransition : IFiniteTransition
    {
        private readonly FiniteState _nextState;

        protected FiniteTransition(FiniteState nextState)
        {
            _nextState = nextState ?? throw new ArgumentNullException(nameof(nextState));
        }

        public bool CanMoveNextState(out FiniteState state)
        {
            state = _nextState;

            return CanTransit();
        }

        protected abstract bool CanTransit();
    }
}
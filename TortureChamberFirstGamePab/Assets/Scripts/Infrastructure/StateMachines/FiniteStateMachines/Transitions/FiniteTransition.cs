using System;
using Scripts.Infrastructure.StateMachines.FiniteStateMachines.States;
using Scripts.InfrastructureInterfaces.StateMachines.FiniteStateMachinew.Transitions;

namespace Scripts.Infrastructure.StateMachines.FiniteStateMachines.Transitions
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
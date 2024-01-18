using System.Collections.Generic;
using Sources.Infrastructure.StateMachines.FiniteStateMachines.Transitions;

namespace Sources.Infrastructure.StateMachines.FiniteStateMachines.States
{
    public abstract class FiniteState
    {
        private readonly List<IFiniteTransition> _transitions = new List<IFiniteTransition>();

        public virtual void Enter()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Exit()
        {
        }

        public void AddTransition(IFiniteTransition transition) =>
            _transitions.Add(transition);

        public void RemoveTransition(IFiniteTransition transition) =>
            _transitions.Remove(transition);

        public bool TryGetNextState(out FiniteState nextSate)
        {
            nextSate = default;

            foreach (IFiniteTransition transition in _transitions)
            {
                if (transition.CanMoveNextState(out nextSate))
                    return true;
            }

            return false;
        }
    }
}
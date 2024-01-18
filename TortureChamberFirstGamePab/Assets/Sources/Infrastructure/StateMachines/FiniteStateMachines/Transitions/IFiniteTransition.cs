using Sources.Infrastructure.StateMachines.FiniteStateMachines.States;

namespace Sources.Infrastructure.StateMachines.FiniteStateMachines.Transitions
{
    public interface IFiniteTransition
    {
        bool CanMoveNextState(out FiniteState state);
    }
}
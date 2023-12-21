using Sources.Infrastructure.StateMachines.States;

namespace Sources.Infrastructure.StateMachines.Transitions
{
    public interface IFiniteTransition
    {
        bool CanMoveNextState(out FiniteState state);
    }
}
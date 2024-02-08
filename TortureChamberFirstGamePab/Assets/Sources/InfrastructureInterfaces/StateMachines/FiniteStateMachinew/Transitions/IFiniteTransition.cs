using Sources.Infrastructure.StateMachines.FiniteStateMachines.States;

namespace Sources.InfrastructureInterfaces.StateMachines.FiniteStateMachinew.Transitions
{
    public interface IFiniteTransition
    {
        bool CanMoveNextState(out FiniteState state);
    }
}
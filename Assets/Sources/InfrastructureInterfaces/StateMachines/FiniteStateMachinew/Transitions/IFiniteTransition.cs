using Infrastructure.StateMachines.FiniteStateMachines.States;

namespace InfrastructureInterfaces.StateMachines.FiniteStateMachinew.Transitions
{
    public interface IFiniteTransition
    {
        bool CanMoveNextState(out FiniteState state);
    }
}
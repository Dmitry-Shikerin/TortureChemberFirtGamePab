using Scripts.Infrastructure.StateMachines.FiniteStateMachines.States;

namespace Scripts.InfrastructureInterfaces.StateMachines.FiniteStateMachinew.Transitions
{
    public interface IFiniteTransition
    {
        bool CanMoveNextState(out FiniteState state);
    }
}
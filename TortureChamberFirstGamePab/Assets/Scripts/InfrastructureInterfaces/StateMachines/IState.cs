using Scripts.InfrastructureInterfaces.Services.UpdateServices;
using Scripts.InfrastructureInterfaces.StateMachines.SceneStateMachines;

namespace Scripts.InfrastructureInterfaces.StateMachines
{
    public interface IState : IEnterable, IExitable, IUpdatable, ILateUpdatable, IFixedUpdatable
    {
    }
}
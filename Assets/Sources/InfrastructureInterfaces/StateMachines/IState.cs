using InfrastructureInterfaces.Services.UpdateServices;
using InfrastructureInterfaces.StateMachines.SceneStateMachines;

namespace InfrastructureInterfaces.StateMachines
{
    public interface IState : IEnterable, IExitable, IUpdatable, ILateUpdatable, IFixedUpdatable
    {
    }
}
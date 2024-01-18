using Sources.InfrastructureInterfaces.Services.UpdateServices;
using Sources.InfrastructureInterfaces.StateMachines.SceneStateMachines;

namespace Sources.InfrastructureInterfaces.StateMachines
{
    public interface IState : IUpdatable, ILateUpdatable, IFixedUpdatable, IEnterable, IExitable
    {
    }
}
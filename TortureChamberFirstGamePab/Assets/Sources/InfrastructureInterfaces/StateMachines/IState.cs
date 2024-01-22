using Sources.InfrastructureInterfaces.Services.UpdateServices;
using Sources.InfrastructureInterfaces.StateMachines.SceneStateMachines;

namespace Sources.InfrastructureInterfaces.StateMachines
{
    public interface IState : IEnterable, IExitable, IUpdatable, ILateUpdatable, IFixedUpdatable
    {
    }
}
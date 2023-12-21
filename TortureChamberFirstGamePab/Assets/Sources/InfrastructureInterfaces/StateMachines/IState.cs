using Sources.InfrastructureInterfaces.Factorys.Services;
using Sources.InfrastructureInterfaces.StateMachines.SceneStateMachines;

namespace Sources.InfrastructureInterfaces.StateMachines
{
    public interface IState : IUpdatable, ILateUpdatable, IFixedUpdatable, IEnterable, IExitable
    {
    }
}
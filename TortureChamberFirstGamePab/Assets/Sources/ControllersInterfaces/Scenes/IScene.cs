using Sources.InfrastructureInterfaces.StateMachines;

namespace Sources.ControllersInterfaces
{
    public interface IScene : IState
    {
        public string Name { get; }
    }
}
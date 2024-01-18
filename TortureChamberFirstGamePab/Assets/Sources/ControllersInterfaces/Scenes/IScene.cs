using Sources.InfrastructureInterfaces.StateMachines;

namespace Sources.ControllersInterfaces.Scenes
{
    public interface IScene : IState
    {
        public string Name { get; }
    }
}
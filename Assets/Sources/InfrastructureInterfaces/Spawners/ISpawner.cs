using PresentationInterfaces.Views;

namespace InfrastructureInterfaces.Spawners
{
    public interface ISpawner<out T>
        where T : IView
    {
        public T Spawn();
    }
}
using Sources.PresentationInterfaces.Views;

namespace Sources.InfrastructureInterfaces.Spawners
{
    public interface ISpawner<out T>
        where T : IView
    {
        public T Spawn();
    }
}
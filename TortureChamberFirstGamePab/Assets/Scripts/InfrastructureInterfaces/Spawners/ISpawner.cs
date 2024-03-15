using Scripts.PresentationInterfaces.Views;

namespace Scripts.InfrastructureInterfaces.Spawners
{
    public interface ISpawner<out T>
        where T : IView
    {
        public T Spawn();
    }
}
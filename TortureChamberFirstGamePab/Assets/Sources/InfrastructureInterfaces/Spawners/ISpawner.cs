namespace Sources.InfrastructureInterfaces.Spawners
{
    public interface ISpawner<out T>
    {
        public T Spawn();
    }
}
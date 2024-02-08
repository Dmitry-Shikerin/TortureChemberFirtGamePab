namespace Sources.InfrastructureInterfaces.Services.ShuffleServices
{
    public interface IShuffleService<out T>
    {
        T GetRandomItem();
    }
}
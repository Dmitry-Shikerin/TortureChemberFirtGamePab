using Domain.Upgrades;

namespace InfrastructureInterfaces.Services.Providers.Upgrades
{
    public interface IMovementProvider
    {
        Upgrader Movement { get; }
    }
}
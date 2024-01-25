using Sources.Domain.Upgrades;

namespace Sources.InfrastructureInterfaces.Services.Providers.Upgrades
{
    public interface IMovementProvider
    {
        Upgrader Movement { get; }
    }
}
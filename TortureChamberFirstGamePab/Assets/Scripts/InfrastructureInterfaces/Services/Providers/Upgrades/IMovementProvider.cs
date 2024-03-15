using Scripts.Domain.Upgrades;

namespace Scripts.InfrastructureInterfaces.Services.Providers.Upgrades
{
    public interface IMovementProvider
    {
        Upgrader Movement { get; }
    }
}
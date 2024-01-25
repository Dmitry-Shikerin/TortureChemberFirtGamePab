using Sources.InfrastructureInterfaces.Services.Providers.Upgrades;

namespace Sources.InfrastructureInterfaces.Services.Providers
{
    public interface IUpgradeProvider : ICharismaProvider, IInventoryProvider, IMovementProvider
    {
    }
}
using Sources.Domain.Upgrades;

namespace Sources.InfrastructureInterfaces.Services.Providers.Upgrades
{
    public interface IInventoryProvider
    {
        Upgrader Inventory { get; }
    }
}
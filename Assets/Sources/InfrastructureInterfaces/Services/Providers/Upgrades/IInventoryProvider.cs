using Domain.Upgrades;

namespace InfrastructureInterfaces.Services.Providers.Upgrades
{
    public interface IInventoryProvider
    {
        Upgrader Inventory { get; }
    }
}
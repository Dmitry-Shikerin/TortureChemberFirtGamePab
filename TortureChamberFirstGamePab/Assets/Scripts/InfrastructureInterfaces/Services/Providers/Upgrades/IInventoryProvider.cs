using Scripts.Domain.Upgrades;

namespace Scripts.InfrastructureInterfaces.Services.Providers.Upgrades
{
    public interface IInventoryProvider
    {
        Upgrader Inventory { get; }
    }
}
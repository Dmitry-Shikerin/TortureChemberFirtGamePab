using Domain.Upgrades;

namespace InfrastructureInterfaces.Services.Providers.Upgrades
{
    public interface IInventorySetter
    {
        void SetInventory(Upgrader inventory);
    }
}
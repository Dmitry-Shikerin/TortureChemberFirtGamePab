using Scripts.Domain.Upgrades;

namespace Scripts.InfrastructureInterfaces.Services.Providers.Upgrades
{
    public interface IInventorySetter
    {
        void SetInventory(Upgrader inventory);
    }
}
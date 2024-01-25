using Sources.Domain.Upgrades;

namespace Sources.InfrastructureInterfaces.Services.Providers.Upgrades
{
    public interface IInventorySetter
    {
        void SetInventory(Upgrader inventory);
    }
}
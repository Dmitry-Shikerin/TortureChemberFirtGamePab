using System;
using Sources.Domain.Upgrades;
using Sources.InfrastructureInterfaces.Services.Providers;
using Sources.InfrastructureInterfaces.Services.Providers.Upgrades;

namespace Sources.Infrastructure.Services.Providers.Upgrades
{
    public class UpgradeProvider : IUpgradeProvider, IUpgradeProviderSetter
    {
        public Upgrader Charisma { get; private set; }
        public Upgrader Inventory { get; private set; }
        public Upgrader Movement { get; private set; }

        public void SetCharisma(Upgrader charisma) => 
            Charisma = charisma ?? throw new ArgumentNullException(nameof(charisma));

        public void SetInventory(Upgrader inventory) => 
            Inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));

        public void SetMovement(Upgrader movement) => 
            Movement = movement ?? throw new ArgumentNullException(nameof(movement));
    }
}
using System;
using Sources.Domain.Upgrades;

namespace Sources.Infrastructure.Services.Brokers
{
    public class PlayerInventoryUpgradeBrokerService
    {
        public Upgrader PlayerInventoryUpgrader { get; private set; }

        public void Set(Upgrader upgrader)
        {
            PlayerInventoryUpgrader = upgrader ?? throw new ArgumentNullException(nameof(upgrader));
        }
    }
}
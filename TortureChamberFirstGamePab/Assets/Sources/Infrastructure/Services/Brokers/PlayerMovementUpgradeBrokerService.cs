using System;
using Sources.Domain.Players;
using Sources.Domain.Upgrades;

namespace Sources.Infrastructure.Services.Brokers
{
    //TODO подумать над названием
    public class PlayerMovementUpgradeBrokerService
    {
        //TODO такое ли решение
        public Upgrader PlayerMovementUpgrader { get; private set; }

        public void Set(Upgrader upgrader)
        {
            PlayerMovementUpgrader = upgrader ?? throw new ArgumentNullException(nameof(upgrader));
        }
    }
}
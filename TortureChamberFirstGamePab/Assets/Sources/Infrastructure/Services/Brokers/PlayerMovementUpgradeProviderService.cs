using System;
using Sources.Domain.Players;
using Sources.Domain.Upgrades;

namespace Sources.Infrastructure.Services.Brokers
{
    //TODO подумать над названием
    public class PlayerMovementUpgradeProviderService
    {
        //TODO такое ли решение
        public Upgrader Movement { get; private set; }

        public void Set(Upgrader upgrader)
        {
            Movement = upgrader ?? throw new ArgumentNullException(nameof(upgrader));
        }
    }
}
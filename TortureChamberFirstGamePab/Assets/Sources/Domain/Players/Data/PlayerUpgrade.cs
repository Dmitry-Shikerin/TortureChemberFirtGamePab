using Sources.Domain.Upgrades;

namespace Sources.Domain.Players.Data
{
    public class PlayerUpgrade
    {
        public PlayerUpgrade(Upgrader charismaUpgrader, Upgrader inventoryUpgrader, Upgrader movementUpgrader)
        {
            CharismaUpgrader = charismaUpgrader;
            InventoryUpgrader = inventoryUpgrader;
            MovementUpgrader = movementUpgrader;
        }

        public Upgrader CharismaUpgrader { get; }
        public Upgrader InventoryUpgrader { get; }
        public Upgrader MovementUpgrader { get; }
    }
}
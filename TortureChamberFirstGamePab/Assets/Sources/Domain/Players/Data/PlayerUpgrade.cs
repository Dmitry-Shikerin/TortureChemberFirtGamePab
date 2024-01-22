using Sources.Domain.Upgrades;

namespace Sources.Domain.Players
{
    public class PlayerUpgrade
    {
        //TODO запрвшивать их под интерфейсом?
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
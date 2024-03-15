using Scripts.Domain.Upgrades;

namespace Scripts.Domain.DataAccess.Containers.Players
{
    public class PlayerUpgrade
    {
        public PlayerUpgrade(Upgrader charisma, Upgrader inventory, Upgrader movement)
        {
            Charisma = charisma;
            Inventory = inventory;
            Movement = movement;
        }

        public Upgrader Charisma { get; }
        public Upgrader Inventory { get; }
        public Upgrader Movement { get; }
    }
}
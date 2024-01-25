using Sources.Domain.Players.PlayerMovements;

namespace Sources.Domain.Players.Data
{
    public class Player
    {
        public Player(PlayerMovement movement, PlayerInventory inventory, PlayerWallet wallet)
        {
            Movement = movement;
            Inventory = inventory;
            Wallet = wallet;
        }
        
        public PlayerMovement Movement { get; private set; }
        public PlayerInventory Inventory { get; private set; }
        public PlayerWallet Wallet { get; private set; }
    }
}
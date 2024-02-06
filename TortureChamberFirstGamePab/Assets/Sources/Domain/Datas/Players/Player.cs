using Sources.Domain.Players;
using Sources.Domain.Players.PlayerMovements;

namespace Sources.Domain.Datas.Players
{
    public class Player
    {
        public Player(PlayerMovement movement, PlayerInventory inventory, PlayerWallet wallet)
        {
            Movement = movement;
            Inventory = inventory;
            Wallet = wallet;
        }
        
        public PlayerMovement Movement { get; }
        public PlayerInventory Inventory { get; }
        public PlayerWallet Wallet { get; }
    }
}
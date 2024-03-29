﻿using Scripts.Domain.Players;
using Scripts.Domain.Players.PlayerMovements;

namespace Scripts.Domain.DataAccess.Containers.Players
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
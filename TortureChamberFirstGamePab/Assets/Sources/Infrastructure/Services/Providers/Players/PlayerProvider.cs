﻿using System;
using Sources.Domain.Players;
using Sources.Domain.Players.PlayerMovements;

namespace Sources.Infrastructure.Services.Providers.Players
{
    public class PlayerProvider : IPlayerProvider, IPlayerProviderSetter
    {
        public PlayerInventory PlayerInventory { get; private set; }
        public PlayerWallet PlayerWallet { get; private set;}
        public PlayerMovement PlayerMovement { get; private set;}
        
        public void SetInventory(PlayerInventory playerInventory)
        {
            PlayerInventory = playerInventory ??
                              throw new NullReferenceException(nameof(playerInventory));
        }

        public void SetWallet(PlayerWallet playerWallet)
        {
            PlayerWallet = playerWallet ?? throw new NullReferenceException(nameof(playerWallet));
        }

        public void SetMovement(PlayerMovement playerMovement)
        {
            PlayerMovement = playerMovement ?? throw new NullReferenceException(nameof(playerMovement));
        }
    }
}
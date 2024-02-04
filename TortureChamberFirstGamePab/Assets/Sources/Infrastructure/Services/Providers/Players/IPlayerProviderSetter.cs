﻿using Sources.Domain.Players;
using Sources.Domain.Players.PlayerMovements;

namespace Sources.Infrastructure.Services.Providers.Players
{
    public interface IPlayerProviderSetter
    {
        void SetInventory(PlayerInventory playerInventory);
        void SetWallet(PlayerWallet playerWallet);
        void SetMovement(PlayerMovement playerMovement);
    }
}
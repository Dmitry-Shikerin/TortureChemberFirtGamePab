using Domain.Players;
using Domain.Players.PlayerMovements;

namespace InfrastructureInterfaces.Services.Providers.Players
{
    public interface IPlayerProviderSetter
    {
        void SetInventory(PlayerInventory playerInventory);
        void SetWallet(PlayerWallet playerWallet);
        void SetMovement(PlayerMovement playerMovement);
    }
}
using Scripts.Domain.Players;
using Scripts.Domain.Players.PlayerMovements;

namespace Scripts.InfrastructureInterfaces.Services.Providers.Players
{
    public interface IPlayerProviderSetter
    {
        void SetInventory(PlayerInventory playerInventory);
        void SetWallet(PlayerWallet playerWallet);
        void SetMovement(PlayerMovement playerMovement);
    }
}
using Scripts.Domain.Players;
using Scripts.Domain.Players.PlayerMovements;

namespace Scripts.InfrastructureInterfaces.Services.Providers.Players
{
    public interface IPlayerProvider
    {
        PlayerInventory PlayerInventory { get; }
        PlayerWallet PlayerWallet { get; }
        PlayerMovement PlayerMovement { get; }
    }
}
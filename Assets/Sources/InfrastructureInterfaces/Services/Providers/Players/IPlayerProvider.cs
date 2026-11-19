using Domain.Players;
using Domain.Players.PlayerMovements;

namespace InfrastructureInterfaces.Services.Providers.Players
{
    public interface IPlayerProvider
    {
        PlayerInventory PlayerInventory { get; }
        PlayerWallet PlayerWallet { get; }
        PlayerMovement PlayerMovement { get; }
    }
}
using Sources.Domain.Players;
using Sources.Domain.Players.PlayerMovements;

namespace Sources.Infrastructure.Services.Providers.Players
{
    public interface IPlayerProvider
    {
        PlayerInventory PlayerInventory { get; }
        PlayerWallet PlayerWallet { get; }
        PlayerMovement PlayerMovement { get; }
    }
}
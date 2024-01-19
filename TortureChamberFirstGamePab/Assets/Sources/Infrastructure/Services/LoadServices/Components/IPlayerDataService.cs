using Sources.Domain.Players;
using Sources.Domain.Players.PlayerMovements;

namespace Sources.Infrastructure.Services.LoadServices.Components
{
    public interface IPlayerDataService
    {
        Player LoadPlayer();
        void Save(Player player);
        void Clear();
    }
}
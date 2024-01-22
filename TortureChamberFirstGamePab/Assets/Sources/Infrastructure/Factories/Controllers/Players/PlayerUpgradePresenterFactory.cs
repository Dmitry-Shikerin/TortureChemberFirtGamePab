using Sources.Controllers.Player;
using Sources.Domain.Players;
using Sources.Domain.Upgrades;
using Sources.PresentationInterfaces.Views.Players;

namespace Sources.Infrastructure.Factories.Controllers.Players
{
    public class PlayerUpgradePresenterFactory
    {
        public PlayerUpgradePresenter Create(Upgrader upgrader, PlayerWallet playerWallet, 
            IPlayerUpgradeView playerUpgradeView)
        {
            return new PlayerUpgradePresenter(upgrader, playerWallet, playerUpgradeView);
        }
    }
}
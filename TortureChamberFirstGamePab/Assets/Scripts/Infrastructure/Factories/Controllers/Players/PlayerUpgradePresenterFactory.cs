using System;
using Scripts.Controllers.Player;
using Scripts.Domain.Players;
using Scripts.Domain.Upgrades;
using Scripts.PresentationInterfaces.Views.Players;

namespace Scripts.Infrastructure.Factories.Controllers.Players
{
    public class PlayerUpgradePresenterFactory
    {
        public PlayerUpgradePresenter Create(
            Upgrader upgrader,
            PlayerWallet playerWallet,
            IPlayerUpgradeView playerUpgradeView)
        {
            if (upgrader == null)
                throw new ArgumentNullException(nameof(upgrader));

            if (playerWallet == null)
                throw new ArgumentNullException(nameof(playerWallet));

            if (playerUpgradeView == null)
                throw new ArgumentNullException(nameof(playerUpgradeView));

            return new PlayerUpgradePresenter(upgrader, playerWallet, playerUpgradeView);
        }
    }
}
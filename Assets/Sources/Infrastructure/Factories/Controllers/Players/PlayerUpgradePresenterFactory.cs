using System;
using Controllers.Player;
using Domain.Players;
using Domain.Upgrades;
using PresentationInterfaces.Views.Players;

namespace Infrastructure.Factories.Controllers.Players
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
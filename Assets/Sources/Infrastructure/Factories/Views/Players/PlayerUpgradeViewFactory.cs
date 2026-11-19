using System;
using Domain.Players;
using Domain.Upgrades;
using Infrastructure.Factories.Controllers.Players;
using Presentation.Views.Player.Upgardes;
using PresentationInterfaces.Views.Players;

namespace Infrastructure.Factories.Views.Players
{
    public class PlayerUpgradeViewFactory
    {
        private readonly PlayerUpgradePresenterFactory _playerUpgradePresenterFactory;

        public PlayerUpgradeViewFactory(PlayerUpgradePresenterFactory playerUpgradePresenterFactory)
        {
            _playerUpgradePresenterFactory = playerUpgradePresenterFactory ??
                                             throw new ArgumentNullException(nameof(playerUpgradePresenterFactory));
        }

        public IPlayerUpgradeView Create(
            Upgrader upgrader, PlayerWallet playerWallet, PlayerUpgradeView playerUpgradeView)
        {
            if (upgrader == null)
                throw new ArgumentNullException(nameof(upgrader));

            if (playerWallet == null)
                throw new ArgumentNullException(nameof(playerWallet));

            if (playerUpgradeView == null)
                throw new ArgumentNullException(nameof(playerUpgradeView));

            var playerUpgradePresenter =
                _playerUpgradePresenterFactory.Create(upgrader, playerWallet, playerUpgradeView);

            playerUpgradeView.Construct(playerUpgradePresenter);

            return playerUpgradeView;
        }
    }
}
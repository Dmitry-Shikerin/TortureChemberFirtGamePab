using System;
using Scripts.Domain.Players;
using Scripts.Domain.Upgrades;
using Scripts.Infrastructure.Factories.Controllers.Players;
using Scripts.Presentation.Views.Player.Upgardes;
using Scripts.PresentationInterfaces.Views.Players;

namespace Scripts.Infrastructure.Factories.Views.Players
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
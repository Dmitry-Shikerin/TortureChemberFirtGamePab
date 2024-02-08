using System;
using Sources.Controllers.Player;
using Sources.Domain.Players;
using Sources.Domain.Upgrades;
using Sources.Infrastructure.Factories.Controllers.Players;
using Sources.Presentation.Views.Player.Upgardes;
using Sources.PresentationInterfaces.Views.Players;

namespace Sources.Infrastructure.Factories.Views.Players
{
    public class PlayerUpgradeViewFactory
    {
        private readonly PlayerUpgradePresenterFactory _playerUpgradePresenterFactory;

        public PlayerUpgradeViewFactory(PlayerUpgradePresenterFactory playerUpgradePresenterFactory)
        {
            _playerUpgradePresenterFactory = playerUpgradePresenterFactory ??
                                             throw new ArgumentNullException(nameof(playerUpgradePresenterFactory));
        }

        public IPlayerUpgradeView Create
        (
            Upgrader upgrader,
            PlayerWallet playerWallet,
            PlayerUpgradeView playerUpgradeView
        )
        {
            if (upgrader == null) 
                throw new ArgumentNullException(nameof(upgrader));
            
            if (playerWallet == null) 
                throw new ArgumentNullException(nameof(playerWallet));
            
            if (playerUpgradeView == null) 
                throw new ArgumentNullException(nameof(playerUpgradeView));
            
            PlayerUpgradePresenter playerUpgradePresenter =
                _playerUpgradePresenterFactory.Create(upgrader, playerWallet, playerUpgradeView);

            playerUpgradeView.Construct(playerUpgradePresenter);

            return playerUpgradeView;
        }
    }
}
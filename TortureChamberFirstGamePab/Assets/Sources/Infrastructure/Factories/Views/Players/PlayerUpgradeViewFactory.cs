﻿using System;
using Sources.Controllers.Player;
using Sources.Domain.Players;
using Sources.Domain.Upgrades;
using Sources.Infrastructure.Factories.Controllers.Players;
using Sources.Presentation.Views.Player.Upgardes;
using Sources.PresentationInterfaces.Views.Players;

namespace Sources.Infrastructure.Factories.Views.Players
{
    //TODO презентер знает что как склеить а логику максимально выносить из контроллера
    //TODO и сервис логики закидывать под интерфейсом
    public class PlayerUpgradeViewFactory
    {
        private readonly PlayerUpgradePresenterFactory _playerUpgradePresenterFactory;

        public PlayerUpgradeViewFactory(PlayerUpgradePresenterFactory playerUpgradePresenterFactory)
        {
            _playerUpgradePresenterFactory = playerUpgradePresenterFactory ?? 
                                             throw new ArgumentNullException(nameof(playerUpgradePresenterFactory));
        }

        public IPlayerUpgradeView Create(Upgrader upgrader, PlayerWallet playerWallet
            , PlayerUpgradeView playerUpgradeView)
        {
            PlayerUpgradePresenter playerUpgradePresenter = 
                _playerUpgradePresenterFactory.Create(upgrader, playerWallet, playerUpgradeView);
            
            playerUpgradeView.Construct(playerUpgradePresenter);

            return playerUpgradeView;
        }
    }
}
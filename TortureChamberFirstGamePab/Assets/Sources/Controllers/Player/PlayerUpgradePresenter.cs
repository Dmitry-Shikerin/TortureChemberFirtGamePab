using System;
using Sources.Domain.Players;
using Sources.Domain.Upgrades;
using Sources.PresentationInterfaces.Views.Players;
using UnityEngine;

namespace Sources.Controllers.Player
{
    public class PlayerUpgradePresenter : PresenterBase
    {
        private readonly Upgrader _upgrader;
        private readonly PlayerWallet _playerWallet;
        private readonly IPlayerUpgradeView _playerUpgradeView;

        public PlayerUpgradePresenter
        (
            Upgrader upgrader,
            PlayerWallet playerWallet,
            IPlayerUpgradeView playerUpgradeView
        )
        {
            _upgrader = upgrader ?? throw new ArgumentNullException(nameof(upgrader));
            _playerWallet = playerWallet ?? throw new ArgumentNullException(nameof(playerWallet));
            _playerUpgradeView = playerUpgradeView ?? throw new ArgumentNullException(nameof(playerUpgradeView));
        }

        private bool CanUpgrade => _playerWallet.Coins.GetValue >
                                   _upgrader.MoneyPerUpgrades[_upgrader.CurrentLevelUpgrade.GetValue];

        public override void Enable()
        {
            UpdatePricePerUpgrade();
            UpdateCurrentLevelUpgrade();
        }

        public void Upgrade()
        {
            try
            {
                if (UpgradeAvailability())
                {
                    _playerWallet.Remove(_upgrader.MoneyPerUpgrades[_upgrader.CurrentLevelUpgrade.GetValue]);
                    _upgrader.Upgrade();
                    UpdatePricePerUpgrade();
                    UpdateCurrentLevelUpgrade();
                }

                UpdatePricePerUpgrade();
                UpdateCurrentLevelUpgrade();
            }
            catch (InvalidOperationException exception)
            {
                Debug.Log(exception.Message);
            }
        }

        private bool UpgradeAvailability()
        {
            if (_upgrader.CurrentLevelUpgrade.GetValue >= _upgrader.MoneyPerUpgrades.Count)
            {
                Debug.Log("Улучшение недоступно");
                return false;
            }

            //TODO порефакторить
            if (_playerWallet.Coins.GetValue < _upgrader.MoneyPerUpgrades[_upgrader.CurrentLevelUpgrade.GetValue])
            {
                Debug.Log("Улучшение недоступно");
                return false;
            }

            return true;
        }

        private void UpdatePricePerUpgrade()
        {
            if (_upgrader.CurrentLevelUpgrade.GetValue >= _upgrader.MoneyPerUpgrades.Count)
            {
                _playerUpgradeView.SetPriceNextUpgrade(
                    $"Цена {_upgrader.MoneyPerUpgrades[_upgrader.CurrentLevelUpgrade.GetValue - 1]}");
                
                return;
            }
                
            _playerUpgradeView.SetPriceNextUpgrade(
                $"Цена {_upgrader.MoneyPerUpgrades[_upgrader.CurrentLevelUpgrade.GetValue]}");
        }

        private void UpdateCurrentLevelUpgrade()
        {
            Debug.Log($"{_upgrader.CurrentLevelUpgrade.StringValue} лвл");
            _playerUpgradeView.SetCurrentLevelUpgrade(
                $"{_upgrader.CurrentLevelUpgrade.StringValue} лвл");
        }
    }
}
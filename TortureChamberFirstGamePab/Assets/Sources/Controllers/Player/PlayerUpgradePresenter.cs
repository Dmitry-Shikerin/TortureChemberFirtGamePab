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
        
        public override void Enable()
        {
            UpdatePricePerUpgrade();
            UpdateCurrentLevelUpgrade();
        }

        public void Upgrade()
        {
            try
            {
                if (CanUpgrade())
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

        private bool CanUpgrade()
        {
            if (_upgrader.CurrentLevelUpgrade.GetValue >= _upgrader.MoneyPerUpgrades.Count)
                return false;

            if (_playerWallet.Coins.GetValue < _upgrader.MoneyPerUpgrades[_upgrader.CurrentLevelUpgrade.GetValue])
                return false;

            return true;
        }

        private void UpdatePricePerUpgrade()
        {
            if (_upgrader.CurrentLevelUpgrade.GetValue >= _upgrader.MoneyPerUpgrades.Count)
            {
                _playerUpgradeView.SetPriceNextUpgrade(
                    $"{_upgrader.MoneyPerUpgrades[_upgrader.CurrentLevelUpgrade.GetValue - 1]}");
                
                return;
            }
                
            _playerUpgradeView.SetPriceNextUpgrade(
                $"{_upgrader.MoneyPerUpgrades[_upgrader.CurrentLevelUpgrade.GetValue]}");
        }

        private void UpdateCurrentLevelUpgrade() =>
            _playerUpgradeView.SetCurrentLevelUpgrade(
                $"{_upgrader.CurrentLevelUpgrade.StringValue}");
    }
}
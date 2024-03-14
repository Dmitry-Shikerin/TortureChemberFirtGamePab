using System;
using Sirenix.Utilities;
using Sources.Domain.Players;
using Sources.Domain.Upgrades;
using Sources.PresentationInterfaces.Views.Players;

namespace Sources.Controllers.Player
{
    public class PlayerUpgradePresenter : PresenterBase
    {
        private readonly IPlayerUpgradeView _playerUpgradeView;
        private readonly PlayerWallet _playerWallet;
        private readonly Upgrader _upgrader;

        public PlayerUpgradePresenter(
            Upgrader upgrader,
            PlayerWallet playerWallet,
            IPlayerUpgradeView playerUpgradeView)
        {
            _upgrader = upgrader ?? throw new ArgumentNullException(nameof(upgrader));
            _playerWallet = playerWallet ?? throw new ArgumentNullException(nameof(playerWallet));
            _playerUpgradeView = playerUpgradeView ?? throw new ArgumentNullException(nameof(playerUpgradeView));
        }

        public override void Enable()
        {
            HideLevelImages();
            ShowLevelImages();
            UpdatePricePerUpgrade();
        }

        public void Upgrade()
        {
            try
            {
                if (CanUpgrade())
                {
                    var moneyPerUpgrade = _upgrader.MoneyPerUpgrades[_upgrader.CurrentLevelUpgrade.GetValue];

                    _playerWallet.Remove(moneyPerUpgrade);
                    _upgrader.Upgrade();
                    UpdatePricePerUpgrade();
                    ShowLevelImages();
                }

                UpdatePricePerUpgrade();
                ShowLevelImages();
            }
            catch (InvalidOperationException exception)
            {
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

        private void ShowLevelImages()
        {
            for (var i = 0; i < _upgrader.CurrentLevelUpgrade.GetValue; i++) _playerUpgradeView.ImageViews[i].Show();
        }

        private void HideLevelImages()
        {
            _playerUpgradeView.ImageViews.ForEach(image => image.Hide());
        }
    }
}
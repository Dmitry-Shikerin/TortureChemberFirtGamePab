using System;
using Sources.Domain.Players;
using Sources.DomainInterfaces.Upgrades;
using Sources.PresentationInterfaces.UI;
using UnityEngine;

namespace Sources.Infrastructure.Services.UpgradeServices
{
    public class PlayerUpgradeService
    {
        private readonly IUpgradeble _upgradeble;
        private readonly ITextUI _levelUpgradeTextUI;
        private readonly ITextUI _priceNextLvlUpgradeTextUI;
        private readonly PlayerWallet _playerWallet;

        //TODO не знал как сделать по другому
        public PlayerUpgradeService(IUpgradeble upgradeble, ITextUI levelUpgradeTextUI,
             ITextUI priceNextLvlUpgradeTextUI, PlayerWallet playerWallet)
        {
            _upgradeble = upgradeble ?? throw new ArgumentNullException(nameof(upgradeble));
            _levelUpgradeTextUI = levelUpgradeTextUI ?? throw new ArgumentNullException(nameof(levelUpgradeTextUI));
            _priceNextLvlUpgradeTextUI = priceNextLvlUpgradeTextUI ?? 
                                         throw new ArgumentNullException(nameof(priceNextLvlUpgradeTextUI));
            _playerWallet = playerWallet ?? throw new ArgumentNullException(nameof(playerWallet));
        }

        public void Start()
        {
            UpdatePricePerUpgrade();
        }
        
        //TODO порефакторить
        public void Upgrade()
        {
            try
            {
                if (UpgradeAvailability())
                {
                    _upgradeble.Upgrade();
                    _playerWallet.Remove(_upgradeble.MoneyPerUpgrades[_upgradeble.CurrentLevelUpgrade.GetValue]);
                    _levelUpgradeTextUI.SetText($"{_upgradeble.CurrentLevelUpgrade.StringValue} уровень");
                    UpdatePricePerUpgrade();
                }
                
                UpdatePricePerUpgrade();
            }
            catch (InvalidOperationException exception)
            {
                //TODO сделать кнопку не активной
                Debug.Log(exception.Message);
            }
        }

        private bool UpgradeAvailability()
        {
            //TODO порефакторить
            if (_playerWallet.Coins.GetValue < _upgradeble.MoneyPerUpgrades[_upgradeble.CurrentLevelUpgrade.GetValue])
            {
                Debug.Log("Улучшение недоступно");
                return false;
            }

            return true;
        }

        private void UpdatePricePerUpgrade()
        {
            _priceNextLvlUpgradeTextUI.SetText($"Цена следующего улучшения " +
                                               $"{_upgradeble.MoneyPerUpgrades[_upgradeble.CurrentLevelUpgrade.GetValue]}");
        }
    }
}